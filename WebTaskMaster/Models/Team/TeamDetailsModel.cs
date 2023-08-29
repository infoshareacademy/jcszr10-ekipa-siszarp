using System.ComponentModel.DataAnnotations;
using System.Drawing.Printing;
using WebTaskMaster.Models.User;

namespace WebTaskMaster.Models.Team
{
    public class TeamDetailsModel
    {
        public Guid TeamId { get; set; }

        [Display(Name = "Name")]
        public TeamNameModel NameModel { get; set; }

        [Display(Name = "Leader")]
        public TeamMemberModel? Leader { get; set; }

        [Display(Name = "Members")]
        public List<TeamMemberModel> Members { get; set; }

        public TeamAddMembersModel AddMembersModel { get; set; }

        public TeamChangeLeaderModel ChangeLeaderModel { get; set; }
    }
}
