using System.ComponentModel.DataAnnotations;

namespace WebTaskMaster.Models.Task
{
	public class MyTasksListModel
	{
        public Guid TaskId { get; set; }

        [Display(Name = "Task name")]
        public string TaskName { get; set; }

        [Display(Name = "Task description")]
        public string? TaskDescription { get; set; }

        public Guid ProjectId { get; set; }

        [Display(Name = "Project name")]
        public string ProjectName { get; set; }

        [Display(Name = "Task status")]
        public string TaskStatus { get; set; }
    }
}
