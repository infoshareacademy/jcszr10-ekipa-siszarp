using Manage_tasks;

namespace WebTaskMaster.Models.Project
{
    public class ProjectDetailsModel : ProjectModel
    {
        public Manage_tasks_Biznes_Logic.Model.Team? ProjectTeam { get; set; }

        public List<Manage_tasks_Biznes_Logic.Model.Team> Teams { get; set; } = new List<Manage_tasks_Biznes_Logic.Model.Team>();
        public List<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
    }
}
