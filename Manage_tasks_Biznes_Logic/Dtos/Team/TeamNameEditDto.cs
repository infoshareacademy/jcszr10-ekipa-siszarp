using System.ComponentModel.DataAnnotations;

namespace Manage_tasks_Biznes_Logic.Dtos.Team;

public class TeamNameEditDto
{
    public Guid TeamId { get; set; }

    public Guid EditorId { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }
}