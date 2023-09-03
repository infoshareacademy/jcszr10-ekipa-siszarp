using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage_tasks_Database.Entities;

public class Project
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public virtual ICollection<Team> Teams { get; set; }
}

