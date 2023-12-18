using Manage_tasks_Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Manage_tasks_Database.EntityConfigurations;

internal class TaskListConfiguration : IEntityTypeConfiguration<TaskListEntity>
{
    public void Configure(EntityTypeBuilder<TaskListEntity> builder)
    {
        builder.ToTable("Task_Lists");

        builder.HasKey(tl => tl.Id);

        builder.Property(tl => tl.Name)
            .HasMaxLength(50);

        builder.HasOne(tl => tl.Project)
            .WithMany(p => p.TaskLists)
            .HasForeignKey(tl => tl.ProjectId)
            .HasPrincipalKey(p => p.Id);
    }
}