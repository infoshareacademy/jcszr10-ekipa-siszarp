namespace Manage_tasks_Biznes_Logic.Dtos.Project;

public class ProjectListForUserDto
{
    public ICollection<ProjectBasicDto> ProjectsOwner { get; set; }

    public ICollection<ProjectBasicDto> ProjectsMember { get; set; }
}