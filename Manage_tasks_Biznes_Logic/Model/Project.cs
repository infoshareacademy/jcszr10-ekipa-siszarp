using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manage_tasks;
using Manage_tasks_Biznes_Logic.Service;

namespace Manage_tasks_Biznes_Logic.Model
{
    public class Project

    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Team ProjectTeam { get; set; }

        public TasksList Tasks { get; set; }    




        public Project(string projectName, string projectDescription)
        {
            Name = projectName;
            Description = projectDescription;
            Tasks = new TasksList();
        }
        public Project() 
        { 
        }

        public void AddTeam(Team team)
        {
            ProjectTeam = team;
        }


    }
}
