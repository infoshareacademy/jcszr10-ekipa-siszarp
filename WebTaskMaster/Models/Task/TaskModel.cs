using Manage_tasks_Biznes_Logic.Model;
using System.ComponentModel.DataAnnotations;
using WebTaskMaster.Models.User;

namespace WebTaskMaster.Models.Task
{
    public class TaskModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime? FinishDate { get; set; }
        public Status Status { get; set; }
        public UserModel? User { get; set; }
    }
}
