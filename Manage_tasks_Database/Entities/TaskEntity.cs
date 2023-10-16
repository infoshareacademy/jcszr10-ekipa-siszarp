using Manage_tasks_Database.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Manage_tasks_Database.Entities;

[EntityTypeConfiguration(typeof(TaskConfiguration))]
public class TaskEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public DateTime? FinishDate { get; set; }

    public TaskStatus StatusId { get; set; }

    public Guid? TaskListId { get; set; }

    public virtual TaskListEntity? TaskList { get; set; }

    public Guid? AssignedUserId { get; set; }

    public virtual UserEntity? AssignedUser { get; set; }
}

