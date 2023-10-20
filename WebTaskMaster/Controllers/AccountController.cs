using System.Security.Claims;
using AutoMapper;
using Manage_tasks_Biznes_Logic.Dtos.Account;
using Manage_tasks_Biznes_Logic.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTaskMaster.Models.Account;

namespace WebTaskMaster.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dto = _mapper.Map<LoginModel, LoginDto>(model);
            var resultDto = await _accountService.LoginAccount(dto);

            if (!resultDto.LoginWasSuccessful)
            {
                return View(model);
            }

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(resultDto.ClaimsIdentity),
                resultDto.AuthProp);

            return RedirectToAction("index", "Home");
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dto = _mapper.Map<RegisterModel, RegisterDto>(model);
            var resultDto = await _accountService.RegisterAccount(dto);
            _mapper.Map(resultDto, model);

            if (model.RegistrationFailed)
            {
                return View(model);
            }

            return RedirectToAction("Login");
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("index", "Home");
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> EditEmail()
        {
            var userIdText = HttpContext.User.Claims
                .Where(c => c.Type == "UserId")
                .Select(c => c.Value)
                .FirstOrDefault();

            if (!Guid.TryParse(userIdText, out var userId))
            {
                return RedirectToAction("Index", "Home");
            }

            var accountEmail = await _accountService.GetAccountEmail(userId);

            var model = new EditEmailModel
            {
                UserId = userId,
                CurrentEmail = accountEmail
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> EditEmail(EditEmailModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dto = _mapper.Map<EditEmailModel, EditEmailDto>(model);
            var resultDto = await _accountService.EditAccountEmail(dto);
            _mapper.Map(resultDto, model);

            if (model.EditEmailFailed)
            {
                return View(model);
            }

            TempData["ToastMessage"] = "Email changed.";

            return RedirectToAction("Details", "User");
        }

        [Authorize(Roles = "User")]
        public ActionResult EditPassword()
        {
            var userIdText = HttpContext.User.Claims
                .Where(c => c.Type == "UserId")
                .Select(c => c.Value)
                .FirstOrDefault();

            if (!Guid.TryParse(userIdText, out var userId))
            {
                return RedirectToAction("Index", "Home");
            }

            var dto = new EditPasswordModel
            {
                UserId = userId
            };

            return View(dto);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> EditPassword(EditPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dto = _mapper.Map<EditPasswordModel, EditPasswordDto>(model);
            var resultDto = await _accountService.EditAccountPassword(dto);
            _mapper.Map(resultDto, model);

            if (model.EditPasswordFailed)
            {
                return View(model);
            }

            TempData["ToastMessage"] = "Password changed.";

            return RedirectToAction("Details", "User");
        }

        [Authorize(Roles = "User")]
        public IActionResult Delete()
        {
            var userIdText = HttpContext.User.Claims
                .Where(c => c.Type == "UserId")
                .Select(c => c.Value)
                .FirstOrDefault();

            if (!Guid.TryParse(userIdText, out var userId))
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new DeleteAccountModel()
            {
                UserId = userId
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Delete(DeleteAccountModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dto = _mapper.Map<DeleteAccountModel, DeleteAccountDto>(model);
            var deletionSuccessful = await _accountService.DeleteAccount(dto);

            if (deletionSuccessful)
            {
                return RedirectToAction("Logout");
            }

            model.DeleteAccountFailed = true;
            return View(model);
        }
    }
}
