using Manage_tasks_Biznes_Logic.Dtos.User;
using Manage_tasks_Biznes_Logic.Model;
using Microsoft.AspNetCore.Mvc;
using Manage_tasks_Biznes_Logic.Service;
using WebTaskMaster.Models.User;
using Microsoft.AspNetCore.Authorization;

namespace WebTaskMaster.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

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

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Details()
        {
            var userIdText = HttpContext.User.Claims
                .Where(c => c.Type == "UserId")
                .Select(c => c.Value)
                .FirstOrDefault();

            if (!Guid.TryParse(userIdText, out var userId))
            {
                return RedirectToAction("Index", "Home");
            }

            var dto = await _userService.GetUserDetails(userId);

            if (dto is null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(dto);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Details(UserDetailsDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            await _userService.EditUserDetails(dto);

            return View(dto);
        }

        public IActionResult Delete(Guid userId)
        {
            _userService.DeleteUser(userId);

            TempData["ToastMessage"] = "User deleted.";

            return RedirectToAction("Index");
        }
    }
}
