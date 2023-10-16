using Manage_tasks_Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manage_tasks_Database.EntityConfigurations;

internal class TaskConfiguration : IEntityTypeConfiguration<TaskEntity>
{
    public void Configure(EntityTypeBuilder<TaskEntity> builder)
    {
        builder.ToTable("Tasks");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Name)
            .HasMaxLength(50);

        builder.Property(t => t.Description)
            .HasMaxLength(500);

        builder.HasOne(t => t.TaskList)
            .WithMany(tl => tl.Tasks)
            .HasForeignKey(t => t.TaskListId)
            .HasPrincipalKey(tl => tl.Id);

        builder.HasOne(t => t.AssignedUser)
            .WithMany(u => u.Tasks)
            .HasForeignKey(t => t.AssignedUserId)
            .HasPrincipalKey(u => u.Id);
    }
}