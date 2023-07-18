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

         Team ProjectTeam { get; set; }

        //public Sprint SprintNumber { get; set; } - do dodania po stworzeniu klasy Sprint

         Project(string projectName, string projectDescription, Team projectTeam)
        {
            ProjectName = projectName;
            ProjectDescription = projectDescription;
            ProjectTeam = projectTeam;
        }
        public Project(string projectName)
        {
            ProjectName = projectName;
        }
        public Project()
        {

        }
    }
}
