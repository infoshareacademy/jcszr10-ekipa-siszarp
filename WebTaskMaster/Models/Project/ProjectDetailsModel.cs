using Manage_tasks_Biznes_Logic.Model;

namespace WebTaskMaster.Models.Project
{
    public class ProjectDetailsModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ProjectTeamModel ProjectTeam { get; set; } = new ProjectTeamModel();

        public List<ProjectTeamModel> Teams { get; set; } = new List<ProjectTeamModel>();
        public TasksList Tasks { get; set; } = new TasksList();
        public ProjectAddTeamModel? ProjectAddTeamModel { get; set; }
        public ProjectModel? ProjectEditModel { get; set; }
    }
}
