using Manage_tasks_Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTaskMaster.Data;
using WebTaskMaster.Models.User;

namespace WebTaskMaster.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegistrationController(ApplicationDbContext context)
        {
            _context = context;
        }



        // GET: RegistrationController


        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Registation( UserRegistrationModel user, CancellationToken cancellationToken)
        {
            //var newCompany = new Company();
            //newCompany.Id = Guid.NewGuid();
            //newCompany.PhoneNumber = user.PhoneNumber;
            //newCompany.CompanyName = user.CompanyName;
            //newCompany.Country = user.Country;
            //newCompany.Email =user.Email;
            //newCompany.UserName = user.UserName;
            //newCompany.PasswordHash = user.PasswordHash;
            
             
            //потрібне хешування пароля

           // Загрузка c базы
         /*   var userFromDb = await _context.ApplicationUsers.FirstOrDefaultAsync(c => c.FirstName == user.FirstName, cancellationToken);
            if (userFromDb is null)
                return NotFound();*/

             //Загрузка в базу
            //await _context.AutorizationDbContext.AddAsync(newCompany, cancellationToken);
            //await _context.SaveChangesAsync(cancellationToken);
            
            return View();
        }

        
    }
}
