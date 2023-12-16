namespace Manage_tasks_Biznes_Logic.Dtos.Task;

public class TaskBasicForUserDto
{
    public Guid TaskId { get; set; }

    public string TaskName { get; set; }

    public string? TaskDescription { get; set; }

    public Guid ProjectId { get; set; }

    public string ProjectName { get; set; }

    public string TaskStatus { get; set; }
}
