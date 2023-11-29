namespace WebTaskMaster.Models.Project
{
	public class ProjectIndexModel
	{
		public List<ProjectBasicModel> ProjectsOwner { get; set; }

		public List<ProjectBasicModel> ProjectsMember { get; set; }
	}
}
