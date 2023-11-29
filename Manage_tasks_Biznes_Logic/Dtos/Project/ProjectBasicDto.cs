namespace Manage_tasks_Biznes_Logic.Dtos.Project;

public class ProjectBasicDto
{
    public Guid ProjectId { get; set; }

    public string Name { get; set; }

    public int CompletionPercent { get; set; }
}