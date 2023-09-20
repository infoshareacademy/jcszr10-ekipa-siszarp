using Manage_tasks_Database.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage_tasks_Database.RegisterUser
{
	public class LoginUserService
	{
		private static List<User> list = new List<User>()
		{
			new User
			{
				Email = "vladbezuglij0@gmail.com",
				Password ="Password"
			}
		};



		public bool IsExist(User user)
		{
			var validation = list.Where(a => a.Email == user.Email && a.Password == user.Password).FirstOrDefault();
			if (validation is not null)
			{
				return true;
			}
			else { return false; }

		}

		public void  AddUserToList(User user)
		{
			list.Add(user);
		}
	}
}
