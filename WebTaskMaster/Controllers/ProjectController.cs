﻿using Manage_tasks_Biznes_Logic.Model;
using Manage_tasks_Biznes_Logic.Service;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Xml.Linq;
using WebTaskMaster.Models.Project;
using WebTaskMaster.Models.Team;

namespace WebTaskMaster.Controllers
{
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
        public ActionResult Index()
        {
            var model = CreateProjectModels();
            return View(model);
        }

        // GET: ProjectController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjectController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProjectModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ActivateModal"] = nameof(Create);

                var models = CreateProjectModels();

                return View("Index", models);
            }

            _projectService.CreateProject(model.Name, model.Description);

            TempData["ToastMessage"] = "Project added.";

            return RedirectToAction("Index");
        }
        private IEnumerable<ProjectModel> CreateProjectModels()
        {
            var projects = _projectService.GetAllProjects().Select(u => new ProjectModel
            {
                Id = u.Id,
                Name = u.Name,
                Description = u.Description
            });

            return projects;
        }

        // GET: ProjectController/Details/5
        public ActionResult Details(Guid projectId)
        {
            var project = _projectService.GetProjectById(projectId);

            if (project is null)
            {
                return RedirectToAction("Index");
            }

            var projectModel = CreateDetailsModel(project);

            return View(projectModel);
        }

        private object CreateDetailsModel(Project project)
        {
            var projectModel = new ProjectDetailsModel
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                ProjectChangeTeamModel = new ProjectChangeTeamModel
                {
                    AvailableTeams = _teamService.GetAllTeams().Select(t => new ProjectTeamModel() { Id = t.Id, Name = t.Name }).ToList()
                }
            };
            if (_teamService.GetTeamById(project.ProjectTeamId) is not null)
            {
                projectModel.ProjectTeam = new ProjectTeamModel
                {
                    Id = _teamService.GetTeamById(project.ProjectTeamId).Id,
                    Name = _teamService.GetTeamById(project.ProjectTeamId).Name,
                    Leader = _teamService.GetTeamById(project.ProjectTeamId).Leader.ToString(),
                };
            }
            else projectModel.ProjectTeam = new ProjectTeamModel();

            return projectModel;
        }

        [HttpPost]
        public IActionResult ChangeTeam(ProjectChangeTeamModel model, Guid projectId)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ActivateModal"] = nameof(ChangeTeam);

                var project = _projectService.GetProjectById(projectId);

                if (project is null)
                {
                    return RedirectToAction("Index");
                }

                var projectModel = CreateDetailsModel(project);

                return View("Details", projectModel);
            }

            _projectService.ChangeTeam(projectId, model.NewTeamId);

            TempData["ToastMessage"] = "Team changed.";

            return RedirectToAction("Details", new { projectId });
        }

        // GET: ProjectController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProjectController/Edit/5


        [HttpPost]
        public IActionResult Edit(ProjectModel model, Guid projectId)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ActivateModal"] = nameof(Edit);

                var project = _projectService.GetProjectById(projectId);

                if (project is null)
                {
                    return RedirectToAction("Index");
                }

                var projectModel = CreateDetailsModel(project);

                return View("Details", projectModel);
            }

            var newDescription = model.Description ?? string.Empty;

            _projectService.EditNameAndDescription(projectId, model.Name, newDescription);

            TempData["ToastMessage"] = "Changes saved.";

            return RedirectToAction("Details", new { projectId });
        }

        public IActionResult Delete(Guid projectId)
        {
            _projectService.RemoveProject(projectId);

            TempData["ToastMessage"] = "Project deleted.";

            return RedirectToAction("Index");
        }
    }
}