using Manage_tasks_Biznes_Logic.Model;
using Microsoft.AspNetCore.Mvc;
using Manage_tasks_Biznes_Logic.Service;
using WebTaskMaster.Models.User;

namespace WebTaskMaster.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
		[Route("user")]
		public IActionResult Index()
        {
            var models = CreateUserModels();

            return View(models);
        }

        [HttpPost]
        public IActionResult Create(UserModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ActivateModal"] = nameof(Create);

                var models = CreateUserModels();

                return View("Index", models);
            }

            var newUser = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Position = model.Position
            };

            _userService.UpdateUser(newUser);

            TempData["ToastMessage"] = "User added.";

            return RedirectToAction("Index");
        }

        private IEnumerable<UserModel> CreateUserModels()
        {
            var users = _userService.GetAllUsers().Select(u => new UserModel
            {
                UserId = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Position = u.Position
            });

            return users;
        }
		[Route("user/{userId:Guid}/details")]
		public IActionResult Details(Guid userId)
        {
            var user = _userService.GetUserById(userId);

            if (user is null)
            {
                return RedirectToAction("Index");
            }

            var userModel = new UserModel
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Position = user.Position
            };

            return View(userModel);
        }

        [HttpPost]
        public IActionResult Edit(UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Details", model);
            }

            var editedUser = new User
            {
                Id = model.UserId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Position = model.Position
            };

            _userService.UpdateUser(editedUser);

            TempData["ToastMessage"] = "Changes saved.";

            return View("Details", model);
        }

        public IActionResult Delete(Guid userId)
        {
            _userService.DeleteUser(userId);

            TempData["ToastMessage"] = "User deleted.";

            return RedirectToAction("Index");
        }
    }
}
