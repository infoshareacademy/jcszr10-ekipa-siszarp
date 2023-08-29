using System.ComponentModel.DataAnnotations;

namespace WebTaskMaster.Models.Team
{
    public class TeamIndexModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        public TeamNameModel NameModel { get; set; }

        [Display(Name = "Leader")]
        public TeamMemberModel? LeaderModel { get; set; }

        [Display(Name = "Number of members:")]
        public int NumOfMembers { get; set; }
    }
}
