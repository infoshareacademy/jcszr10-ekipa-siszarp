namespace WebTaskMaster.Models.Task
{
    public class MoveTaskModel
    {
        public Guid DestinationId { get; set; }
        public String TasksIds { get; set; }
        public string url { get; set; }
    }
}
