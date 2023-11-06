using Manage_tasks_Database.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Manage_tasks_Database.Entities;

[EntityTypeConfiguration(typeof(ProjectConfiguration))]
public class ProjectEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

#warning Disallow nullable on OwnerId property.
    public Guid? OwnerId { get; set; }

#warning Disallow nullable on Owner property.
    public virtual UserEntity? Owner { get; set; }

    public virtual ICollection<TeamEntity> Teams { get; set; }

    public virtual ICollection<TaskListEntity> TaskLists { get; set; }
}

