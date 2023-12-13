using Manage_tasks_Biznes_Logic.Model;

namespace WebTaskMaster.Models.Project
{
    public class ProjectDetailsModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ProjectTeamModel ProjectTeam { get; set; } = new ProjectTeamModel();

        public List<TasksList> Tasks { get; set; } = new List<TasksList>();
        public ProjectAddTeamModel? ProjectAddTeamModel { get; set; }
        public ProjectModel? ProjectEditModel { get; set; }
        public Guid UserID { get; set; }

        public int GetCountCurrentStatusTask(string currentTask)
        {
	        int currentStatusCount = 0;
	        int allTasksCount = 0;
	        int result = 0;

			foreach (var item in  Tasks)
	        {
		        currentStatusCount += item.GetCountCurrentStatusTasks(currentTask , out int allCountInList);
		        allTasksCount += allCountInList;

			}
			if (allTasksCount != 0)
			{
				result = (100 / allTasksCount) * currentStatusCount;

			}

			if (result == 0)
			{

				result = 1;
			}

			return result;
		}
        

}
}
