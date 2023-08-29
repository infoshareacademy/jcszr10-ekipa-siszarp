using System.ComponentModel.DataAnnotations;

namespace WebTaskMaster.Models.Team
{
    public class TeamNameModel
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Enter team {0}")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "{0} must contain between {2} and {1} characters.")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [StringLength(200, ErrorMessage = "{0} can contain no more than {2} characters.")]
        public string? Description { get; set; }
    }
}
