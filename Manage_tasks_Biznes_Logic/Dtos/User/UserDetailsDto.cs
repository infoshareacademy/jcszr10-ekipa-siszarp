using System.ComponentModel.DataAnnotations;

namespace Manage_tasks_Biznes_Logic.Dtos.User;

public class UserDetailsDto
{
    public Guid UserId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string? Position { get; set; }

    public DateTime? DateOfBirth { get; set; }
}