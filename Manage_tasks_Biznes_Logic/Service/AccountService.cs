using Manage_tasks_Biznes_Logic.Dtos;
using Manage_tasks_Database.Context;
using Manage_tasks_Database.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Manage_tasks_Biznes_Logic.Service
{
	public interface IAccountService
	{
		Task RegisterUser(RegisterDto user);

		Task<ClaimsPrincipal> LoginUser(LoginDto model);

	}

	public class AccountService : IAccountService  //Controller
	{
		private readonly DataBaseContext _dbContex;

		public AccountService(DataBaseContext dbContex)
		{
			_dbContex = dbContex;
		}

		public async Task RegisterUser(RegisterDto user)
		{
			//validacja 
			//PasswordHash = user.Password  robimy hash
			if (user == null)
			{
				
			}
			else
			{
				var newUser = new User()
				{
					FirstName = "Vlad"/*user.FirstName*/,
					LastName = "Vlad" /*user.LastName*/,
					Email = user.Email,
					PasswordHash = user.Password,
					Id = Guid.NewGuid(),

				};
				await _dbContex.Users.AddAsync(newUser);
				await _dbContex.SaveChangesAsync();
			}



		}

		public async Task<ClaimsPrincipal> LoginUser(LoginDto model)
		{

			var user = await _dbContex.Users
				.Where(a => a.Email == model.Email && a.PasswordHash == model.Password)
				.FirstOrDefaultAsync();
			
			//var user = await AuthenticateUser(model.Email, model.Password);


			var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, user.Email),
					new Claim("FullName", user.Id.ToString()),
					new Claim(ClaimTypes.Role, "Administrator"),
				};

			var claimsIdentity = new ClaimsIdentity(
				claims, CookieAuthenticationDefaults.AuthenticationScheme);

			var Claims = new ClaimsPrincipal(claimsIdentity);

			return Claims;
		}






	}
}
