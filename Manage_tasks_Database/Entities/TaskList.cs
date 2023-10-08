using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage_tasks_Database.Entities;

public class TaskList
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid ProjectId { get; set; }

    public virtual Project Project { get; set; }
}

