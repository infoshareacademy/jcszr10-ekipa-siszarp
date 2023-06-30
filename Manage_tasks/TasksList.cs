namespace Manage_tasks
{
    /// <summary>
    /// Klasa obsługująca listę zadań.
    /// </summary>
    public class TasksList
    {
        private int Id { get; } //do przyszłej obsługi bazy danych


        //kolejne właściwości do dodania jeśli zajdzie taka potrzeba
        public List<Task> Tasks { get; set;}
        public string TasksListName { get; set; }
        //
        /// <summary>
        /// Podstawowy konstruktor do tworzenia listy zadań.
        /// </summary>
        /// <param name="TasksListName">Nazwa listy zadań.</param>
        public TasksList(string TasksListName)
        {
            Tasks = new List<Task>();
            this.TasksListName = TasksListName;
        }
        /// <summary>
        /// Metoda dodająca obiekt klasy Task do listy zadań.
        /// </summary>
        /// <param name="NewTask">Obiekt klasy Task</param>
        public void AddTask(Task NewTask)
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
        public void RemoveTask(Task NewTask)
        { 
            Tasks.Remove(NewTask); 
        }
        /// <summary>
        /// Metoda do wybierania zadania z listy zadań.
        /// </summary>
        /// <param name="IndexOfTask">Indeks z listy Tasks</param>
        /// <returns>Zwraca zmienną klasy Task</returns>
        public Task PickTask(int IndexOfTask)
        {
            Task Task = Tasks[IndexOfTask];

            return Task;
        }
            
     
    }
}
