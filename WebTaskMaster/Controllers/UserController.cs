using Manage_tasks_Biznes_Logic.Dtos.User;
using Microsoft.AspNetCore.Mvc;
using Manage_tasks_Biznes_Logic.Service;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using WebTaskMaster.Models.User;

namespace WebTaskMaster.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
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
            var model = _mapper.Map<UserDetailsDto, UserDetailsModel>(dto);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Details(UserDetailsModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dto = _mapper.Map<UserDetailsModel, UserDetailsDto>(model);
            await _userService.EditUserDetails(dto);

            TempData["ToastMessage"] = "Changed saved.";

            return View(model);
        }
    }
}
