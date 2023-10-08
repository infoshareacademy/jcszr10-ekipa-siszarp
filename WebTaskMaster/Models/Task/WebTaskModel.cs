using Manage_tasks_Biznes_Logic.Model;

namespace WebTaskMaster.Models.Task
{
    public class WebTaskModel
    {
        public ProjectTask ProjectTask { get; set; }
        public string[] newValues {  get; set; }
        public WebTaskModel()
        {
            newValues = new string[5];
        }

    }
}
