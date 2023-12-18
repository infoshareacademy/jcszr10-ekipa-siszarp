namespace WebTaskMaster.Models.Task
{
    public class NewTasksListModel
    {
        public Guid ProjectId { get; set; }
        public Guid TasksListId {  get; set; }
        public string TasksListName { get; set; }
        public string url { get; set; }
    }
}
