using Manage_tasks;
using Manage_tasks_Biznes_Logic.Data;

namespace Manage_tasks_Biznes_Logic.Model
{
   
    public class Project 

    {
        
        public string Name { get; set; }
        public string Description { get; set; }
        public Team ProjectTeam { get; set; }

        public TasksList Tasks { get; set; }

        public int OutID { get; set; }

        public Project() 
        {
            Tasks = new TasksList();
            
            
        }

        public Project(string projectName, string projectDescription) :this()
        {
            Name = projectName;
            Description = projectDescription;
            
        }
        

        public void AddTeam(Team team)
        {
            ProjectTeam = team;
        }

        public void Crash()
        {
            Name = "Error";
            Description ="Error";
        }
    }
}
