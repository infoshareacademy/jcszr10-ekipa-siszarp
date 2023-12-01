using Manage_tasks_Biznes_Logic.Model;

namespace WebTaskMaster.Models.Project
{
    public class ProjectDetailsModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ProjectTeamModel ProjectTeam { get; set; } = new ProjectTeamModel();

        public List<TasksList> Tasks { get; set; } = new List<TasksList>();
        public ProjectAddTeamModel? ProjectAddTeamModel { get; set; }
        public ProjectModel? ProjectEditModel { get; set; }
        public Guid UserID { get; set; }
    }
}
