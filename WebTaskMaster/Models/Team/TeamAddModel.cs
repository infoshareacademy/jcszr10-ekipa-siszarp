using System.ComponentModel.DataAnnotations;

namespace Manage_tasks_Biznes_Logic.Dtos.Team;

public class TeamAddModel
{
    [Required]
    public bool fromCreateProjectView { get; set; }

    [Display(Name = "Team name")]
    [Required(ErrorMessage = "Enter {0}")]
    [StringLength(17, MinimumLength = 3, ErrorMessage = "{0} must contain between {2} and {1} characters.")]
    public string Name { get; set; }

    [Display(Name = "Team description")]
    [StringLength(500, ErrorMessage = "{0} can contain no more than {2} characters.")]
    public string? Description { get; set; }
}