using Manage_tasks;
using Microsoft.Build.Experimental.ProjectCache;

namespace WebTaskMaster.Models.Project
{
    public class ProjectDetailsModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProjectTeamModel ProjectTeam { get; set; } = new ProjectTeamModel();

        public List<ProjectTeamModel> Teams { get; set; } = new List<ProjectTeamModel>();
        public List<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
        public ProjectChangeTeamModel? ProjectChangeTeamModel { get; set; }
        public ProjectModel? ProjectEditModel { get; set; }
    }
}
