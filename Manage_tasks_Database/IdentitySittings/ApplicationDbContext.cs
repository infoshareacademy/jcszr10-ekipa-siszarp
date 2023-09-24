using Manage_tasks_Database.Entities;
using Manage_tasks_Database.Identity.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Manage_tasks_Database.Identity
{
	public class ApplicationDbContext : IdentityDbContext
	{
		DbSet<Company> companies { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.Entity<Team>()
			   .HasKey(c => c.Id);
			builder.Entity<Team>()
				.HasIndex(c => c.Id);

			builder.Entity<IdentityUser>(options =>
			{
				options.ToTable("Users");
			});

			builder.Entity<Company>(options =>
			{
				options.ToTable(name: "Company");
			});
			builder.Entity<IdentityRole>(options =>
			{
				options.ToTable(name: "Role");
			});
			builder.Entity<IdentityUserRole<string>>(options =>
			{
				options.ToTable("UserRoles");
			});
			builder.Entity<IdentityUserClaim<string>>(options =>
			{
				options.ToTable("UserClaims");
			});
			builder.Entity<IdentityUserLogin<string>>(options =>
			{
				options.ToTable("UserLogins");
			});
			builder.Entity<IdentityRoleClaim<string>>(options =>
			{
				options.ToTable("RoleClaims");
			});
			builder.Entity<IdentityUserToken<string>>(options =>
			{
				options.ToTable("UserToken");
			});

			//Отношение к базе даных (нужно почитать)
			/*            modelBuilder.Entity<Manage_tasks_Database.Entities.Task>()
                            .HasOne(c => c.AssignedUser);*/
		}
	}
}
