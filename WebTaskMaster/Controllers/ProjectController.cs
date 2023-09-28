using Manage_tasks_Biznes_Logic.Model;
using Manage_tasks_Biznes_Logic.Service;
using Microsoft.AspNetCore.Mvc;
using WebTaskMaster.Models.Project;

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
        public ActionResult Details(Guid Id)
        {
            var project = _projectService.GetProjectById(Id);

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
                ProjectTeam = project.ProjectTeam,
                //Tasks = project.Tasks
            };

            return projectModel;
        }


        // GET: ProjectController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProjectController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProjectController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProjectController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        //private IEnumerable<ProjectTeamModel> ChangeTeamModel()
        //{
        //    var teams = _teamService.GetAllTeams().Select(u => new ProjectTeamModel
        //    {
        //        Id = u.Id,
        //        TeamName = u.Name
        //    });

        //    return teams;
        //}
    }
}
