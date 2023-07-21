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
    //NAZWA, OPIS, TEAM, SPRINT
    {
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public Team ProjectTeam { get; set; }

        

        //public Sprint SprintNumber { get; set; } - do dodania po stworzeniu klasy Sprint

        public Project(string projectName, string projectDescription)
        {
            ProjectName = projectName;
            ProjectDescription = projectDescription;
            
        }

        public Project(string projectName)
        {
            ProjectName = projectName;
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
