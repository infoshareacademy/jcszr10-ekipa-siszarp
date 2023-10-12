using Manage_tasks_Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manage_tasks_Database.EntityConfigurations;

internal class ProjectConfiguration : IEntityTypeConfiguration<ProjectEntity>
{
    public void Configure(EntityTypeBuilder<ProjectEntity> builder)
    {
        builder.ToTable("Projects");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasMaxLength(50);

        builder.Property(p => p.Description)
            .HasMaxLength(500);

        builder.HasMany(p => p.Teams)
            .WithMany(t => t.Projects)
            .UsingEntity<ProjectTeamEntity>(
                l => l.HasOne<TeamEntity>().WithMany().HasForeignKey(pt => pt.TeamId).HasPrincipalKey(t => t.Id),
                r => r.HasOne<ProjectEntity>().WithMany().HasForeignKey(pt => pt.ProjectId).HasPrincipalKey(p => p.Id));
    }
}