using Manage_tasks_Biznes_Logic.Dtos;
using Manage_tasks_Biznes_Logic.Model;
using Manage_tasks_Biznes_Logic.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Manage_tasks_Database.Context;

namespace WebTaskMaster.Controllers
{
    public class AccountController : Controller
    {
	    private readonly IAccountService _accServ;
	    private readonly DataBaseContext _dbContex;

		public AccountController(IAccountService AccServ)
        {
            _accServ = AccServ;
        }
		[AllowAnonymous]
		public IActionResult Login()
        {
            return View();

		}

		[AllowAnonymous]
		[HttpPost]
        public async Task<IActionResult> Login(LoginDto model)
        {
			var claims = await _accServ.LoginUser(model);
			 

			var authProperties = new AuthenticationProperties
				{
					AllowRefresh = true,


					ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),


					IsPersistent = true,


					IssuedUtc = DateTimeOffset.Now,


					RedirectUri = "Home/Index"
					// The full path or absolute URI to be used as an http 
					// redirect response value.
				};

				await HttpContext.SignInAsync(
					CookieAuthenticationDefaults.AuthenticationScheme,
					new ClaimsPrincipal(claims),
					authProperties);
				
				return View();
		}


		

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDto model)
        {
	        await _accServ.RegisterUser(model); 

            return RedirectToAction("Login");
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: AccountController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountController/Edit/5
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

        // GET: AccountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountController/Delete/5
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
