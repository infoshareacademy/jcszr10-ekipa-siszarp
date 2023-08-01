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
        internal DateTime FinishDate;
        internal User AssignedUser;
        


        public ProjectTask() { }
        /// <summary>
        /// Podstawowy konstruktor do tworzenia nowego zadania.
        /// </summary>
        /// <param name="TaskName">Parametr przedstawiający nazwę zadania.</param>
        /// <param name="TaskDescription">Parametr przedstawiający opis zadania.</param>        
        public ProjectTask(string TaskName, string TaskDescription)
        {
            this.TaskName = TaskName;
            this.TaskDescription = TaskDescription;
            Status = new Status();
            
        }
        /// <summary>
        /// Metoda klasy zwracająca tablice z właściwościami.
        /// </summary>
        public string[] TaskDetails()
        {
            if(this.AssignedUser == null && FinishDate == null)
            {
                return new string[]
                {
                    this.TaskName, this.TaskDescription, this.Status.ShowCurrentStatus(), string.Empty, string.Empty
                };
            }
            else if(this.AssignedUser == null)
            {
                return new string[]
                {
                    this.TaskName, this.TaskDescription, this.Status.ShowCurrentStatus(), string.Empty, this.AssignedUser.FirstName
                };
            }
            else if(this.FinishDate == null)
            {
                return new string[]
                {
                    this.TaskName, this.TaskDescription, this.Status.ShowCurrentStatus(), this.FinishDate.ToString(), string.Empty
                };
            }
            else
            {
                return new string[]
                {
                    this.TaskName, this.TaskDescription, this.Status.ShowCurrentStatus(), this.FinishDate.ToString(), this.AssignedUser.FirstName
                };
            }
        }
        
        
        
        
        
    }
}
