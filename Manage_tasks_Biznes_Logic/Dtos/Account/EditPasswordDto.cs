using System.ComponentModel.DataAnnotations;

namespace Manage_tasks_Biznes_Logic.Dtos.Account;

public class EditPasswordDto
{
    [Required]
    public Guid UserId { get; set; }

    [Display(Name = "Current password")]
    [Required(ErrorMessage = "Enter {0}.")]
    [MinLength(3, ErrorMessage = "{0} must have at least {1} characters.")]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; }

    [Display(Name = "New password")]
    [Required(ErrorMessage = "Enter {0}.")]
    [MinLength(3, ErrorMessage = "{0} must have at least {1} characters.")]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }

    [Display(Name = "Repeat new password")]
    [Required(ErrorMessage = "{0}.")]
    [MinLength(3, ErrorMessage = "{0} must have at least {1} characters.")]
    [DataType(DataType.Password)]
    public string ConfirmNewPassword { get; set; }

    public bool EditPasswordFailed { get; set; }

    public bool WrongCurrentPassword { get; set; }

    public bool NewPasswordIsOldPassword { get; set; }

    public bool PasswordsAreEqual { get; set; }

}