using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage_tasks_Database.Entities;

internal class ProjectTeam
{
    public Guid ProjectId { get; set; }

    public Guid TeamId { get; set; }
}

