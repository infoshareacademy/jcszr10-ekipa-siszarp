using Manage_tasks_Biznes_Logic.Model;

namespace WebTaskMaster.Models.Task
{
    public class WebTaskModel
    {
        public Guid TasksListId { get; set; }
        public Guid ProjectId { get; set; }
        public Manage_tasks_Biznes_Logic.Model.Team? Team { get; set; }
        public ProjectTask ProjectTask { get; set; }
        public string[] newValues {  get; set; }
        public WebTaskModel()
        {
            newValues = new string[5];
        }
        public string url { get; set; }

    }
}
