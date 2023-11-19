using AutoMapper;
using Manage_tasks_Biznes_Logic.Dtos.Team;
using Manage_tasks_Biznes_Logic.Model;
using Manage_tasks_Biznes_Logic.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebTaskMaster.Extensions;
using WebTaskMaster.Models.Project;
using WebTaskMaster.Models.Team;

namespace WebTaskMaster.Controllers
{
	[Authorize(Roles = "User")]
	public class ProjectController : Controller
	{
		private readonly IProjectService _projectService;
		private readonly ITeamService _teamService;
		private readonly IMapper _mapper;


		public ProjectController(IProjectService projectService, ITeamService teamService, IMapper mapper)
		{
			_projectService = projectService;
			_teamService = teamService;
			_mapper = mapper;
		}

		// GET: ProjectController
		[Route("project")]
		[Authorize(Roles = "User")]
		public async Task<IActionResult> Index()
		{

			if (!HttpContext.User.Claims.TryGetAuthenticatedUserId(out var userId))
			{
				return RedirectToAction("Index", "Home");
			}

			var userPartOfTeam = await _teamService.GetAllTeamIdUserPartOfAsync(userId);   

			var projects = await _projectService.GetAllProjects();
			

			var ownerProjectList = projects
				.Where(a => a.OwnerId == userId)
				.Select(a => new ProjectModel() { Id = a.Id, Name = a.Name, Description = a.Description }).ToList();

			var memberProjectList = projects
				.Where(a => a.ProjectTeams
					.Select(team => team.Id)
					.Any(teamId => userPartOfTeam
						.Contains(teamId)))
				.Select(a => new ProjectModel() { Id = a.Id, Description = a.Description, Name = a.Name })
				.ToList();

			 

			var finalyModel = new ProjectOwnerAndMember()
			{
				owner = ownerProjectList,
				member = memberProjectList

			};

			return View(finalyModel);
		}

		// GET: ProjectController/Add
		public ActionResult Create()
		{
			return View();
		}

		// POST: ProjectController/Add
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(ProjectModel model)
		{
			if (!ModelState.IsValid)
			{
				ViewData["ActivateModal"] = nameof(Create);

				var models = CreateProjectModels();

				return View("Index", models);
			}

			HttpContext.User.Claims.TryGetAuthenticatedUserId(out var UserId);

			model.Description ??= string.Empty;
			await _projectService.CreateProject(model.Name, model.Description, UserId);

			TempData["ToastMessage"] = "Project added.";

			return RedirectToAction("Index");
		}
		private async Task<IEnumerable<ProjectModel>> CreateProjectModels()
		{
			var projects = await _projectService.GetAllProjects();
			List<ProjectModel> projectModels = new();
			foreach (var project in projects)
			{
				var projectModel = new ProjectModel
				{
					Id = project.Id,
					Name = project.Name,
					Description = project.Description
				};
				projectModels.Add(projectModel);
			}
			return projectModels;
		}

		// GET: ProjectController/Details/5
		[Route("project/{projectId:Guid}/details")]
		public async Task<IActionResult> Details(Guid projectId)
		{
			var project = await _projectService.GetProjectById(projectId);

			if (project is null)
			{
				return RedirectToAction("Index");
			}

			var projectModel = await CreateDetailsModel(project);

			return View(projectModel);
		}

		private async Task<ProjectDetailsModel> CreateDetailsModel(Project project)
		{
			var teams = project.ProjectTeams.Select(t => new ProjectTeamModel
			{
				Id = t.Id,
				Name = t.Name
			})
				.ToList();
			var addTeamModel = new ProjectAddTeamModel
			{
				AvailableTeams = (await _teamService.GetAllTeams())
						.Select(t => new ProjectTeamModel
						{
							Id = t.Id,
							Name = t.Name,
							//Leader = t.Leader
						})
						.ToList()
			};
			var projectModel = new ProjectDetailsModel
			{
				Id = project.Id,
				Name = project.Name,
				Description = project.Description,
				Teams = teams,
				Tasks = project.Tasks,
				ProjectAddTeamModel = addTeamModel
			};
			if (await _teamService.GetTeamById(project.ProjectTeamId) is null)
			{
				projectModel.ProjectTeam = new ProjectTeamModel();
			}
			return projectModel;
		}

		[HttpPost]
		[Authorize(Roles = "User")]
		public async Task<IActionResult> AddTeam(ProjectAddTeamModel model, Guid projectId)
		{
			if (!ModelState.IsValid)
			{
				ViewData["ActivateModal"] = nameof(AddTeam);

				var project = await _projectService.GetProjectById(projectId);

				if (project is null)
				{
					return RedirectToAction("Index");
				}

				var projectModel = await CreateDetailsModel(project);

				return View("Details", projectModel);
			}

			await _projectService.AddTeamToProject(projectId, model.TeamsIdsToAdd);

			TempData["ToastMessage"] = "Team added.";

			return RedirectToAction("Details", new { projectId });
		}

		[Authorize(Roles = "User")]

		[Route("project/{projectId:Guid}/details/deleteTeam/{teamId:Guid}")]
		public async Task<IActionResult> DeleteTeam(Guid projectId, Guid teamId)
		{
			await _projectService.DeleteTeamFromProject(projectId, teamId);

			TempData["ToastMessage"] = "Team removed.";

			return RedirectToAction("Details", new { projectId });
		}

		[HttpPost]
		[Authorize(Roles = "User")]
		[Route("project/{projectId:Guid}/edit")]
		public async Task<IActionResult> Edit(ProjectModel model, Guid projectId)
		{
			if (!ModelState.IsValid)
			{
				ViewData["ActivateModal"] = nameof(Edit);

				var project = await _projectService.GetProjectById(projectId);

				if (project is null)
				{
					return RedirectToAction("Index");
				}

				var projectModel = await CreateDetailsModel(project);

				return View("Details", projectModel);
			}

			var newDescription = model.Description ?? string.Empty;

			await _projectService.EditNameAndDescription(projectId, model.Name, newDescription);

			TempData["ToastMessage"] = "Changes saved.";

			return RedirectToAction("Details", new { projectId });
		}

		[Authorize(Roles = "User")]
		public async Task<IActionResult> Delete(Guid projectId)
		{
			await _projectService.RemoveProject(projectId);

			TempData["ToastMessage"] = "Project deleted.";

			return RedirectToAction("Index");
		}
	}
}
