using System.ComponentModel.DataAnnotations;

namespace WebTaskMaster.Models.Project;

public class ProjectBasicModel
{
    public Guid ProjectId { get; set; }

    [Display(Name = "Name")]
    public string Name { get; set; }

    [Display(Name = "Completion percent")]
    public int CompletionPercent { get; set; }
}