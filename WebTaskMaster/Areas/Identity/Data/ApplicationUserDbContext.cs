using Manage_tasks_Database.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebTaskMaster.Areas.Identity.Data;

namespace WebTaskMaster.Data;

public class ApplicationUserDbContext : IdentityDbContext<CompanyUser>
{

     
    public ApplicationUserDbContext(DbContextOptions<ApplicationUserDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        RenameIdentityTables(builder);
    }
    protected void RenameIdentityTables(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("CstUserMngt");
        builder.Entity<CompanyUser>(entity =>
        {
            entity.ToTable(name: "CompanyUser");
        });
        builder.Entity<IdentityUser>(entity =>
        {
            entity.ToTable(name: "Users");
        });
        builder.Entity<IdentityRole>(entity =>
        {
            entity.ToTable(name: "Roles");
        });
        builder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.ToTable("UserRoles");
        });
        builder.Entity<IdentityUserClaim<string>>(entity =>
        {
            entity.ToTable("UserClaims");
        });
        builder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.ToTable("UserLogins");
        });
        builder.Entity<IdentityRoleClaim<string>>(entity =>
        {
            entity.ToTable("RoleClaims");
        });
        builder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.ToTable("UserTokens");
        });
    }
}
