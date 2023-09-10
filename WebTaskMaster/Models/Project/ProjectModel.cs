using Manage_tasks;
using Manage_tasks_Biznes_Logic.Model;
using System.ComponentModel.DataAnnotations;


namespace WebTaskMaster.Models.Project
{
    public class ProjectModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Manage_tasks_Biznes_Logic.Model.Team ProjectTeam { get; set; }

        public TasksList Tasks { get; set; }
        public List<Manage_tasks_Biznes_Logic.Model.Team> teams { get; set; }
    }
}
