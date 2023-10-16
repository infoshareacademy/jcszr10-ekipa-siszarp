using Manage_tasks_Database.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Manage_tasks_Database.Entities;

[EntityTypeConfiguration(typeof(TaskListConfiguration))]
public class TaskListEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid ProjectId { get; set; }

    public virtual ProjectEntity Project { get; set; }

    public virtual ICollection<TaskEntity> Tasks { get; set; }
}

