using System.Security.Claims;
using Manage_tasks_Biznes_Logic.Dtos.Account;
using Manage_tasks_Biznes_Logic.Dtos.User;
using Manage_tasks_Biznes_Logic.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebTaskMaster.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accServ;

        public AccountController(IAccountService AccServ)
        {
            _accServ = AccServ;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            await _accServ.LoginUser(dto);

            if (!dto.LoginWasSuccessful)
            {
                return View(dto);
            }

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(dto.ClaimsIdentity),
                dto.AuthProp);

            return RedirectToAction("index", "Home");
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            await _accServ.RegisterUser(dto);

            if (dto.RegistrationFailed)
            {
                return View(dto);
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

            var dto = await _accServ.GetEditEmail(userId);

            if (dto is null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(dto);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> EditEmail(EditEmailDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            await _accServ.EditEmail(dto);

            if (dto.EditEmailFailed)
            {
                return View(dto);
            }

            return RedirectToAction("Details", "User");
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> EditPassword()
        {
            var userIdText = HttpContext.User.Claims
                .Where(c => c.Type == "UserId")
                .Select(c => c.Value)
                .FirstOrDefault();

            if (!Guid.TryParse(userIdText, out var userId))
            {
                return RedirectToAction("Index", "Home");
            }

            var dto = new EditPasswordDto
            {
                UserId = userId
            };

            return View(dto);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> EditPassword(EditPasswordDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            await _accServ.EditPassword(dto);

            if (dto.EditPasswordFailed)
            {
                return View(dto);
            }

            return RedirectToAction("Details", "User");
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteAccount()
        {
            var userIdText = HttpContext.User.Claims
                .Where(c => c.Type == "UserId")
                .Select(c => c.Value)
                .FirstOrDefault();

            if (!Guid.TryParse(userIdText, out var userId))
            {
                return RedirectToAction("Index", "Home");
            }

            var dto = new DeleteAccountDto()
            {
                UserId = userId
            };

            return View(dto);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteAccount(DeleteAccountDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            await _accServ.DeleteAccount(dto);

            if (dto.DeleteAccountFailed)
            {
                return View(dto);
            }

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}
