using System.ComponentModel.DataAnnotations;

namespace WebTaskMaster.Models.Team
{
    public class TeamMemberModel
    {
        public Guid Id { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }
    }
}
