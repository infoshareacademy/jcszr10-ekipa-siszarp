using Manage_tasks_Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebTaskMaster.Data
{
	public class ApplicationDbContext : DbContext
	{
        

        public DbSet<Company> ApplicationCompany { get; set; }
         

        public ApplicationDbContext()
        {     
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Company>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Company>()
                .HasIndex(c => c.Id);
            

             //Отношение к базе даных (нужно почитать)
/*            modelBuilder.Entity<Manage_tasks_Database.Entities.Task>()
                .HasOne(c => c.AssignedUser);*/

            
        }

    }
	 
}
