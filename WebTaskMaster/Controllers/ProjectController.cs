using Manage_tasks_Biznes_Logic.Model;
using Manage_tasks_Biznes_Logic.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebTaskMaster.Models.Project;
using WebTaskMaster.Models.Team;
using WebTaskMaster.Models.User;
using Manage_tasks_Biznes_Logic.Service;

namespace WebTaskMaster.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ProjectService _projectService;
        
        public ProjectController()
        {
            _projectService = new ProjectService();
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

            var newProject = new Project
            {
                Name = model.Name,
                Description = model.Description,
                ProjectTeam = model.ProjectTeam,
                Tasks = model.Tasks
            };

            _projectService.SaveProjectToJson();

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
        public ActionResult Details(int id)
        {
            return View();
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
    }
}
