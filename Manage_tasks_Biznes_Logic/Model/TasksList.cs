

using System.Drawing.Text;

namespace Manage_tasks_Biznes_Logic.Model
{
	/// <summary>
	/// Klasa obsługująca listę zadań.
	/// </summary>
	public class TasksList
	{

		private IEditTask _editTask;
		public Guid Id { get; set; } //do przyszłej obsługi bazy danych
									 //kolejne właściwości do dodania jeśli zajdzie taka potrzeba
		public List<ProjectTask>? Tasks { get; set; }
		public string TasksListName { get; set; }
		//

		public TasksList()
		{
			Tasks = new List<ProjectTask>();
		}
		/// <summary>
		/// Podstawowy konstruktor do tworzenia listy zadań.
		/// </summary>
		/// <param name="TasksListName">Nazwa listy zadań.</param>
		public TasksList(string TasksListName) : this()
		{
			this.TasksListName = TasksListName;
		}


		public int GetCountCurrentStatusTasks(string currentStatus, out int allCountInList)
		{
			allCountInList = Tasks.Count();

			var TaskCount = Tasks.Where(a => a.Status.CurrentStatus == currentStatus).Count();

			return TaskCount;
		}


		/// <summary>
		/// Metoda dodająca obiekt klasy Task do listy zadań.
		/// </summary>
		/// <param name="NewTask">Obiekt klasy Task</param>
		public void AddTask(ProjectTask NewTask)
		{
			Tasks.Add(NewTask);
		}

		/// <summary>
		/// Metoda obsługująca usuwanie zadania.
		/// </summary>
		/// <param name="Task"></param>
		public void RemoveTask(ProjectTask Task)
		{
			Tasks.Remove(Task);
		}
		/// <summary>
		/// Metoda do wybierania zadania z listy zadań.
		/// </summary>
		/// <param name="IndexOfTask">Indeks z listy Tasks</param>
		/// <returns>Zwraca zmienną klasy Task</returns>
		public ProjectTask GetTaskByGuid(Guid taskGuid)
		{
			ProjectTask Task = Tasks.FirstOrDefault(x => x.Id == taskGuid);

			return Task;
		}

		public void AssignUser(User user, int taskIndex)
		{
			Tasks[taskIndex].AssignedUser = user;
		}


		public TasksList(IEditTask edit)
		{
			_editTask = edit;
		}
		public void EditTask(string newValue, ProjectTask task)
		{
			_editTask.EditTask(newValue, task);
		}



	}
}
