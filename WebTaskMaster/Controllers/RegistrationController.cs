using Manage_tasks_Database.Entities;
using Manage_tasks_Database.RegisterUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebTaskMaster.Models.User;

namespace WebTaskMaster.Controllers
{
    public class RegistrationController : Controller
    {
        LoginUserService userNew = new LoginUserService();

        // GET: RegistrationController

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registation(UserRegistrationModel user)
        {
            if(user != null && user.Password.Length > 6)
            {  
                userNew.AddUserToList(Build(user));
            }
            return View();
        }

        public User Build(UserRegistrationModel user)
        {
            var NewUser = new User();
            NewUser.Password = user.Password;
            NewUser.Email = user.Email;
            NewUser.FirstName = user.FirstName;
            return NewUser;
        }
    }
}
