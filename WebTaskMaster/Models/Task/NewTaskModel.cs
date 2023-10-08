using Manage_tasks_Biznes_Logic.Model;
namespace WebTaskMaster.Models.Task
{
    public class NewTaskModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public TasksList TasksList { get; set; }
    }
}
