using Manage_tasks_Database.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Manage_tasks_Database.Entities;

[EntityTypeConfiguration(typeof(ProjectTeamConfiguration))]
public class ProjectTeamEntity
{
    public Guid ProjectId { get; set; }

    public Guid TeamId { get; set; }
}

