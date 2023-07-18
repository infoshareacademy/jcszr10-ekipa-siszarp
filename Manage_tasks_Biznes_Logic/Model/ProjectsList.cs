using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage_tasks_Biznes_Logic.Model
{
    public class ProjectsList
    {
        public Task SetTask { get; set; }
        List<Project> Projects { get; set; }

        public ProjectsList()
        {
            Projects = new List<Project>();
        }

    }
}
