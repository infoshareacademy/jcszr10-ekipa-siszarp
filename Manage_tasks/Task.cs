namespace Manage_tasks
{
    /// <summary>
    /// Klasa obsługująca zadanie.
    /// </summary>
    public class Task
    {
        private int Id { get; } //do przyszłej obsługi bazy danych 
        
        //kolejne właściwości do dodania
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        //

        /// <summary>
        /// Podstawowy konstruktor do tworzenia zadania.
        /// </summary>
        /// <param name="TaskName">Parametr przedstawiający nazwę zadania.</param>
        /// <param name="TaskDescription">Parametr przedstawiający opis zadania.</param>
        public Task(string TaskName, string TaskDescription)
        {
            this.TaskName = TaskName;
            this.TaskDescription = TaskDescription;
        }
        /// <summary>
        /// Metoda klasy wypisująca szczegóły wybranego zadania.
        /// </summary>
        public void TaskDetails()
        {
            //opis pozostaje do zmiany po ustaleniu wyglądu
            Console.WriteLine(TaskName + " " + TaskDescription);
        }
        
        
    }
}
