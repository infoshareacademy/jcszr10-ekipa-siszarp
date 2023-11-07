using Manage_tasks_Biznes_Logic.Dtos.User;
using Microsoft.AspNetCore.Mvc;
using Manage_tasks_Biznes_Logic.Service;
using Microsoft.AspNetCore.Authorization;

namespace WebTaskMaster.Controllers
{
	[Authorize(Roles = "User")]
	public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
		[Route("user")]
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
    }
}
