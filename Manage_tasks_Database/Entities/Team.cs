using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage_tasks_Database.Entities;

public class Team
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Guid? LeaderId { get; set; }

    public virtual User? Leader { get; set; }

    public virtual ICollection<User> Members { get; set; }
}

