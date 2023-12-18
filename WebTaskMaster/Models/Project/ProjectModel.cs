using Manage_tasks_Biznes_Logic.Model;
using NuGet.Frameworks;
using System.ComponentModel.DataAnnotations;
using WebTaskMaster.Models.Team;

namespace WebTaskMaster.Models.Project
{
    public class ProjectModel
    {
        public Guid Id { get; set; }
        [StringLength(maximumLength: 17, MinimumLength = 3)]
        public string Name { get; set; }
        public string? Description { get; set; }
        public TeamBasicModel? Team { get; set; }
    }
}
