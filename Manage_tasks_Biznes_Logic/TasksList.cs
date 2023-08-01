namespace Manage_tasks
{
    /// <summary>
    /// Klasa obsługująca listę zadań.
    /// </summary>
    public class TasksList
    {
        private int Id { get; } //do przyszłej obsługi bazy danych
        private IEditTask _editTask;

        //kolejne właściwości do dodania jeśli zajdzie taka potrzeba
        public List<ProjectTask> Tasks { get; set;}
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
        
        /// <summary>
        /// Metoda dodająca obiekt klasy Task do listy zadań.
        /// </summary>
        /// <param name="NewTask">Obiekt klasy Task</param>
        public void AddTask(ProjectTask NewTask)
        {           
            Tasks.Add(NewTask);
        }
        /// <summary>
        /// Metoda zwracająca szczegóły wszystkich zadań w liście zadań.
        /// </summary>
        public void DisplayTasksDetails()
        {
            foreach (var Task in Tasks) 
            {
                Task.TaskDetails();
            }
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
        public ProjectTask PickTask(int IndexOfTask)
        {
            ProjectTask Task = Tasks[IndexOfTask];

            return Task;
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
