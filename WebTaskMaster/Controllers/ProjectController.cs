using Manage_tasks_Biznes_Logic.Model;
using Manage_tasks_Biznes_Logic.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTaskMaster.Models.Project;

namespace WebTaskMaster.Controllers
{
	[Authorize(Roles = "User")]
	public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly ITeamService _teamService;

        public ProjectController(IProjectService projectService, ITeamService teamService)
        {
            _projectService = projectService;
            _teamService = teamService;
        }

        // GET: ProjectController
        [Route("project")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Index()
        {
            var user = User.Claims;

            var model = await CreateProjectModels();
            return View(model);
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
            model.Description ??= string.Empty;
            await _projectService.CreateProject(model.Name, model.Description);

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
