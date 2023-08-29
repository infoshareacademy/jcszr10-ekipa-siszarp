using System.ComponentModel.DataAnnotations;

namespace WebTaskMaster.Models.User
{
    public class UserModel
    {
        public Guid UserId { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "Enter {0}.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "{0} must contain between {2} and {1} characters.")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Enter {0}.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "{0} must contain between {2} and {1} characters.")]
        public string LastName { get; set; }

        [Display(Name = "Position")]
        [Required(ErrorMessage = "Enter {0}.")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "{0} must contain between {2} and {1} characters.")]
        public string Position { get; set; }
    }
}
