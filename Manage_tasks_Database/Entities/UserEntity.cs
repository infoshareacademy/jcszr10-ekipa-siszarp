using Manage_tasks_Database.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Manage_tasks_Database.Entities;

[EntityTypeConfiguration(typeof(UserConfiguration))]
public class UserEntity  
{
	public Guid Id { get; set; }

    public string Email { get; set; }

    public byte[] PasswordSalt { get; set; }

    public string PasswordHash { get; set; }

    public string FirstName { get; set; }

	public string LastName { get; set; }

	public string? Position { get; set; }

	public DateTime? DateOfBirth { get; set; }

    public virtual ICollection<TeamEntity> Teams { get; set; }

    public virtual ICollection<TeamEntity> TeamsLeader { get; set; }

    public virtual ICollection<TaskEntity> Tasks { get; set; }
}

