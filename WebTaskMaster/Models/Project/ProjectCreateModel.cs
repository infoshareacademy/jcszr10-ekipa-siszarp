using System.ComponentModel.DataAnnotations;
using WebTaskMaster.Models.Team;

namespace WebTaskMaster.Models.Project;

public class ProjectCreateModel
{
    [Display(Name = "Project name")]
    [Required(ErrorMessage = "Enter {0}")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} must contain between {2} and {1} characters.")]
    public string Name { get; set; }

    [Display(Name = "Project description")]
    [StringLength(500, ErrorMessage = "{0} can contain no more than {2} characters.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Project team.")]
    public Guid TeamId { get; set; }

    [Display(Name = "Available teams")]
    public ICollection<TeamBasicModel>? AvailableTeams { get; set; }
}