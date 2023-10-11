using Manage_tasks_Biznes_Logic.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manage_tasks_Database.Context;
using Manage_tasks_Database.Entities;

namespace Manage_tasks_Biznes_Logic.Service
{
	public interface IAcoountService
	{
		  Task RegisterUser(RegisterDto user);
		 
	}
	public class AccountService : IAcoountService
	{
		private readonly DataBaseContext _dbContex;
        public AccountService(DataBaseContext dbContex)
        {
            _dbContex = dbContex;
        }

       public  async Task RegisterUser(RegisterDto user)
		{
			//validacja 
			//PasswordHash = user.Password  robimy hash
			
			var newUser = new User()
			{
				FirstName = user.FirstName,
				LastName = user.LastName,
				Email = user.Email,
				PasswordHash = user.Password,
				Id = Guid.NewGuid(),

			};
			await _dbContex.Users.AddAsync(newUser);
			await _dbContex.SaveChangesAsync();



		}

		public async  Task LoginUser(User user)
		{


		} 

	}
}
