using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace WebTaskMaster.Models.Account;

public class LoginModel
{
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

    [Display(Name = "Remember me")]
    [Required]
    public bool RememberMe { get; set; }

    public bool LoginWasSuccessful { get; set; }
}