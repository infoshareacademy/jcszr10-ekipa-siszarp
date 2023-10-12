using Manage_tasks_Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manage_tasks_Database.EntityConfigurations;

internal class TeamUserConfiguration : IEntityTypeConfiguration<TeamUserEntity>
{
    public void Configure(EntityTypeBuilder<TeamUserEntity> builder)
    {
        builder.ToTable("Teams_Users");

        builder.HasKey(tu => new { tu.TeamId, tu.UserId });
    }
}