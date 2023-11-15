using System.ComponentModel.DataAnnotations;

namespace WebTaskMaster.Models.Account;

public class EditEmailModel
{
    [Required]
    public Guid UserId { get; set; }

    [Display(Name = "Current email")]
    [Required]
    [DataType(DataType.EmailAddress)]
    public string CurrentEmail { get; set; }

    [Display(Name = "New email")]
    [Required(ErrorMessage = "Enter {0}.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} must contain between {2} and {1} characters.")]
    [DataType(DataType.EmailAddress)]
    public string NewEmail { get; set; }

    public bool EditEmailFailed { get; set; }

    public bool NewEmailAlreadyInUse { get; set; }

    public bool NewEmailIsCurrentEmail { get; set; }
}