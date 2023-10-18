using System.ComponentModel.DataAnnotations;

namespace Manage_tasks_Biznes_Logic.Dtos.Account;

public class DeleteAccountDto
{
    [Required]
    public Guid UserId { get; set; }

    [Display(Name = "Password")]
    [Required(ErrorMessage = "Enter {0}.")]
    [MinLength(3, ErrorMessage = "{0} must have at least {1} characters.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public bool DeleteAccountFailed { get; set; }

    public bool WrongPassword { get; set; }
}