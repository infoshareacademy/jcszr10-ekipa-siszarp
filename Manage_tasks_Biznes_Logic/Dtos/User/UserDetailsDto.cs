using System.ComponentModel.DataAnnotations;

namespace Manage_tasks_Biznes_Logic.Dtos.User;

public class UserDetailsDto
{
    [Required]
    public Guid UserId { get; set; }

    [Display(Name = "First name")]
    [Required(ErrorMessage = "Enter {0}.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} must contain between {2} and {1} characters.")]
    public string FirstName { get; set; }

    [Display(Name = "Last name")]
    [Required(ErrorMessage = "Enter {0}.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} must contain between {2} and {1} characters.")]
    public string LastName { get; set; }

    [Display(Name = "Position")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} must contain between {2} and {1} characters.")]
    public string? Position { get; set; }

    [Display(Name = "Date of birth")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? DateOfBirth { get; set; }

    public bool ChangesSaved { get; set; }
}