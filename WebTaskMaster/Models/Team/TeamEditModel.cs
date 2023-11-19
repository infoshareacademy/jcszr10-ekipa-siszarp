using System.ComponentModel.DataAnnotations;

namespace WebTaskMaster.Models.Team;

public class TeamEditModel
{
    [Required]
    public Guid TeamId { get; set; }

    [Display(Name = "Name")]
    [Required(ErrorMessage = "Enter {0}")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} must contain between {2} and {1} characters.")]
    public string Name { get; set; }

    [Display(Name = "Description")]
    [StringLength(500, ErrorMessage = "{0} can contain no more than {2} characters.")]
    public string? Description { get; set; }
}