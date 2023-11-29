using Manage_tasks_Database.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Manage_tasks_Database.Entities;

[EntityTypeConfiguration(typeof(ProjectConfiguration))]
public class ProjectEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public Guid TeamId { get; set; }

    public virtual TeamEntity Team { get; set; }

    public virtual ICollection<TaskListEntity> TaskLists { get; set; }
}

