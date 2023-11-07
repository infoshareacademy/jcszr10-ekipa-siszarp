using Manage_tasks_Biznes_Logic.Dtos.Team;
using System.ComponentModel.DataAnnotations;

namespace WebTaskMaster.Models.Team;

public class TeamBasicModel
{
    public Guid TeamId { get; set; }

    [Display(Name = "Name")]
    public string Name { get; set; }

    [Display(Name = "Description")]
    public string? Description { get; set; }

    [Display(Name = "Leader")]
    public TeamMemberModel Leader { get; set; }

    //[Display(Name = "Number of members")]
    //public int NumberOfMembers { get; set; }
}