using Manage_tasks_Biznes_Logic.Model;
using System.ComponentModel.DataAnnotations;

namespace WebTaskMaster.Models.Task
{
    public class NewTaskModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public Guid TasksListId { get; set; }
        public Guid ProjectId { get; set; }
    }
}
