using System.ComponentModel.DataAnnotations;

namespace WebTaskMaster.Models.Account;

public class RegisterModel
{
    [Display(Name = "First name")]
    [Required(ErrorMessage = "Enter {0}.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} must contain between {2} and {1} characters.")]
    public string FirstName { get; set; }

    [Display(Name = "Last name")]
    [Required(ErrorMessage = "Enter {0}.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} must contain between {2} and {1} characters.")]
    public string LastName { get; set; }

    [Display(Name = "Email")]
    [Required(ErrorMessage = "Enter {0}.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "{0} must contain between {2} and {1} characters.")]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Display(Name = "Password")]
    [Required(ErrorMessage = "Enter {0}.")]
    [MinLength(3, ErrorMessage = "{0} must have at least {1} characters.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Repeat password")]
    [Required(ErrorMessage = "{0}.")]
    [MinLength(3, ErrorMessage = "{0} must have at least {1} characters.")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }

    public bool RegistrationFailed { get; set; }

    public bool PasswordsAreEqual { get; set; }

    public bool EmailAlreadyInUse { get; set; }
}