using System;
using Manage_tasks_Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Manage_tasks_Database.Context
{
	public class DataBaseContext : DbContext
	{
		public DbSet<UserEntity> UserEntities { get; set; }

		public DbSet<TeamEntity> TeamEntities { get; set; }

		public DbSet<TeamUserEntity> TeamUserEntities { get; set; }

		public DbSet<ProjectEntity> ProjectEntities { get; set; }

		public DbSet<ProjectTeamEntity> ProjectTeamEntities { get; set; }

		public DbSet<TaskListEntity> TaskListEntities { get; set; }

		public DbSet<TaskEntity> TaskEntities { get; set; }

		public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)  
		{
			
		}

	}
}
