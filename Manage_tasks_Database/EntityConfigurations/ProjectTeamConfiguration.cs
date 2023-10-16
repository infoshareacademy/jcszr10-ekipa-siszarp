using Manage_tasks_Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manage_tasks_Database.EntityConfigurations;

internal class ProjectTeamConfiguration : IEntityTypeConfiguration<ProjectTeamEntity>
{
    public void Configure(EntityTypeBuilder<ProjectTeamEntity> builder)
    {
        builder.ToTable("Projects_Teams");

        builder.HasKey(pt => new { pt.ProjectId, pt.TeamId });
    }
}