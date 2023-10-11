using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manage_tasks_Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Manage_tasks_Database.Context
{
	public class DataBaseContext : DbContext
	{
		public DbSet<User> Users { get; set; } 
		public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)  
		{
			
		}

	}
}
