using System.ComponentModel.DataAnnotations;

namespace WebTaskMaster.Models.Project
{
    public class ProjectTeamModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Team Name")]
        public string Name { get; set; }
        [Display(Name = "Team Leader")]
        public string Leader { get; set; }
    }
}

