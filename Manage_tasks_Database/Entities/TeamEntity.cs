using Manage_tasks_Database.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Manage_tasks_Database.Entities;

[EntityTypeConfiguration(typeof(TeamConfiguration))]
public class TeamEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public Guid? LeaderId { get; set; }

    public virtual UserEntity? Leader { get; set; }

    public virtual ICollection<UserEntity> Members { get; set; }

    public virtual ICollection<ProjectEntity> Projects { get; set; }
}

