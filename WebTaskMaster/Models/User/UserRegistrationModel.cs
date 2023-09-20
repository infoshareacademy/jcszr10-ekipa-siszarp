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
		public string FirstName { get; set; }

		[Display(Name = "Last name")]
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
	}
}

