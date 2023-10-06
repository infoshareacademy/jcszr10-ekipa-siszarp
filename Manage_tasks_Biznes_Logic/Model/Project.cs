using Manage_tasks;

namespace Manage_tasks_Biznes_Logic.Model
{

    public class Project

    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Team ProjectTeam { get; set; }
        public Guid ProjectTeamId { get; set; }

        public TasksList Tasks { get; set; }

        public int OutID { get; set; }
        public Guid Id { get; set; }

        public Project()
        {
            Tasks = new TasksList();
            Id = new Guid();
        }

        public Project(string projectName, string projectDescription) : this()
        {
            Name = projectName;
            Description = projectDescription;
            Id = new Guid();
        }


        public void AddTeam(Team team)
        {
            ProjectTeam = team;
            ProjectTeamId = Guid.Empty;
        }

        public void Crash()
        {
            Name = "Error";
            Description = "Error";
        }
    }
}
