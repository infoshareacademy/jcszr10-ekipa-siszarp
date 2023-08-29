using System.ComponentModel.DataAnnotations;

namespace WebTaskMaster.Models.Team
{
    public class TeamAddMembersModel
    {
        [Required(ErrorMessage = "Select at least one member.")]
        public List<Guid> MembersIdsToAdd { get; set; }

        [Display(Name = "Available members")]
        public List<TeamMemberModel>? AvailableMembers { get; set; }
    }
}
