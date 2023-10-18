using Manage_tasks_Database.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Manage_tasks_Database.Entities;

[EntityTypeConfiguration(typeof(TeamUserConfiguration))]
public class TeamUserEntity
{
    public Guid TeamId { get; set; }

    public Guid UserId { get; set; }
}

