using Manage_tasks_Biznes_Logic.Dtos.User;
using Microsoft.AspNetCore.Mvc;
using Manage_tasks_Biznes_Logic.Service;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using WebTaskMaster.Models.User;
using WebTaskMaster.Extensions;

namespace WebTaskMaster.Controllers
{
	[Authorize(Roles = "User")]
	public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
		[Route("user")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Details()
        {
            if (!HttpContext.User.Claims.TryGetAuthenticatedUserId(out var userId))
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

            TempData.SetSuccessToastMessage("Changed saved.");

            return View(model);
        }
    }
}
