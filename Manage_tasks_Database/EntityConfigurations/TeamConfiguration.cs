using Manage_tasks_Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manage_tasks_Database.EntityConfigurations;

internal class TeamConfiguration : IEntityTypeConfiguration<TeamEntity>
{
    public void Configure(EntityTypeBuilder<TeamEntity> builder)
    {
        builder.ToTable("Teams");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .HasMaxLength(50);

        builder.Property(t => t.Description)
            .HasMaxLength(500);

        builder.HasMany(t => t.Members)
            .WithMany(u => u.Teams)
            .UsingEntity<TeamUserEntity>(
                l => l.HasOne<UserEntity>().WithMany().HasForeignKey(tu => tu.TeamId).HasPrincipalKey(t => t.Id),
                r => r.HasOne<TeamEntity>().WithMany().HasForeignKey(tu => tu.UserId).HasPrincipalKey(u => u.Id));

        builder.HasOne(t => t.Leader)
            .WithMany(u => u.TeamsLeader)
            .HasForeignKey(t => t.LeaderId)
            .HasPrincipalKey(u => u.Id);
    }
}