using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage_tasks_Database.Entities;

public class TeamUser
{
    public Guid TeamId { get; set; }

    public Guid UserId { get; set; }
}

