using Manage_tasks_Biznes_Logic.Model;
using NuGet.Frameworks;
using System.ComponentModel.DataAnnotations;


namespace WebTaskMaster.Models.Project
{
    public class ProjectModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
