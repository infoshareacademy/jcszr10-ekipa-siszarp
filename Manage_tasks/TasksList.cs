namespace Manage_tasks
{
    /// <summary>
    /// Klasa obsługująca listę zadań.
    /// </summary>
    public class TasksList
    {
        private int Id { get; } //do przyszłej obsługi bazy danych


        //kolejne właściwości do dodania jeśli zajdzie taka potrzeba
        public List<ProjectTask> Tasks { get; set;}
        public string TasksListName { get; set; }
        //
        /// <summary>
        /// Podstawowy konstruktor do tworzenia listy zadań.
        /// </summary>
        /// <param name="TasksListName">Nazwa listy zadań.</param>
        public TasksList(string TasksListName)
        {
            Tasks = new List<ProjectTask>();
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
        /// <param name="NewTask"></param>
        public void RemoveTask(ProjectTask NewTask)
        { 
            Tasks.Remove(NewTask); 
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
            
     
    }
}
