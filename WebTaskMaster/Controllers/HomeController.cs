using Manage_tasks_Database.Entities;
using Manage_tasks_Database.RegisterUser;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebTaskMaster.Models;

namespace WebTaskMaster.Controllers 
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly LoginUserService LoginService;
       

        public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
			LoginService = new LoginUserService();
		}

		public IActionResult Login()
		{
			return View("Index");
		}
		[HttpPost]
		public IActionResult Login(User model)
		{
			if (!ModelState.IsValid && !LoginService.IsExist(model))
			{
				TempData["ToastMessage"] = "Nieprawidłowy login lub hasło";

				return RedirectToAction("Index");
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