using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace Manage_tasks_Biznes_Logic.Dtos.Account
{
    public class LoginDto
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

        [MemberNotNullWhen(true, nameof(UserId), nameof(AuthProp), nameof(ClaimsIdentity))]
        public bool LoginWasSuccessful { get; set; }

        public Guid? UserId { get; set; }

        public AuthenticationProperties? AuthProp { get; set; }

        public ClaimsIdentity? ClaimsIdentity { get; set; }
    }
}
