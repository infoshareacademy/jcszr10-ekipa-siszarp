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
        public TasksList TasksList { get; set; }
    }
}
