using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage_tasks_Database.Entities;

internal class Task
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime? FinishDate { get; set; }

    public int StatusId { get; set; }

    public Guid? TaskListId { get; set; }

    public virtual TaskList? TaskList { get; set; }

    public Guid? AssignedUserId { get; set; }

    public virtual User? AssignedUser { get; set; }
}

