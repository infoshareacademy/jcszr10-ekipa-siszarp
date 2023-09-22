using Manage_tasks_Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebTaskMaster.Models;
using WebTaskMaster.Models.User;

namespace WebTaskMaster.Controllers 
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly SignInManager<Company> _signInManager;
		private readonly UserManager<Company> _userManager;
       

        public HomeController(ILogger<HomeController> logger , UserManager<Company> company , SignInManager<Company> signInManager)
		{
			_logger = logger;
			_userManager = company;
			_signInManager = signInManager;
        }

		public   IActionResult Login()
		{
			return View("Index");
		}
		[HttpPost]
		public async Task<IActionResult> Login(UserRegistrationModel model)
		{
			if (!ModelState.IsValid)
			{
				TempData["ToastMessage"] = "Nieprawidłowy login lub hasło";

				return RedirectToAction("Index");
			}
			var existedUser = await _userManager.FindByEmailAsync(model.Email);
			var result = await _signInManager.PasswordSignInAsync(existedUser, model.Password, true, false);
			if (!result.Succeeded)
			{
				return BadRequest("You are not allowed");
			}

			TempData["ToastMessage"] = "Success!";

			return RedirectToAction("Index");
		}

      
        public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}
		 
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}