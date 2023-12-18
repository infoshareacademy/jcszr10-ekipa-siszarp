using Manage_tasks_Biznes_Logic.Model;

namespace WebTaskMaster.Models.Task
{
    public class WebTasksList
    {
        public TasksList? Tasks { get; set; }
        
        public Guid? TeamId { get; set; }
    }
}
