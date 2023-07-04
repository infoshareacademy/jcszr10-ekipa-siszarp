namespace Manage_tasks
{
    /// <summary>
    /// Klasa obsługująca zadanie.
    /// </summary>
    public class ProjectTask
    {
        private int Id { get; } //do przyszłej obsługi bazy danych 

        //kolejne właściwości do dodania
        internal string TaskName;
        internal string TaskDescription;
        public Status Status;
        internal DateTime DueDate;
        //

        /// <summary>
        /// Podstawowy konstruktor do tworzenia zadania.
        /// </summary>
        /// <param name="TaskName">Parametr przedstawiający nazwę zadania.</param>
        /// <param name="TaskDescription">Parametr przedstawiający opis zadania.</param>
        /// <param name="dueDate">Parametr wyrażający maksymalną date ukończenia.</param>
        public ProjectTask(string TaskName, string TaskDescription, DateTime dueDate)
        {
            this.TaskName = TaskName;
            this.TaskDescription = TaskDescription;
            Status = new Status();
            DueDate = dueDate;
        }
        /// <summary>
        /// Metoda klasy wypisująca szczegóły wybranego zadania.
        /// </summary>
        public virtual void TaskDetails()
        {
            //opis pozostaje do zmiany po ustaleniu wyglądu
            Console.WriteLine(TaskName + " " + TaskDescription + " " + DueDate + " " + Status.ShowCurrentStatus());
        }
        
        
        
        
        
    }
}
