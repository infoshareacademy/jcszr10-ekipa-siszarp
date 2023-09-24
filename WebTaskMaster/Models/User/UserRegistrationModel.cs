using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace WebTaskMaster.Models.User
{
    public class UserRegistrationModel
    {
        public Guid Id { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "Enter {0}.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "{0} must contain between {2} and {1} characters.")]
        public string UserName { get; set; }

        [Display(Name = "Surname")]
        [Required(ErrorMessage = "Enter {0}.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "{0} must contain between {2} and {1} characters.")]
        public string LastName { get; set; }    

        public DateTime DateOfBirth { get; set; }

        public string? Position { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Enter {0}.")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "{0} must contain between {2} and {1} characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter password.")]
        [StringLength(50, MinimumLength = 7, ErrorMessage = "{0} must contain between {2} and {1} characters.")]
        public string Password { get; set; }

       
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }


        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        public string Country { get; set; }
        public string PasswordHash { get; set; }
        
    }
}

