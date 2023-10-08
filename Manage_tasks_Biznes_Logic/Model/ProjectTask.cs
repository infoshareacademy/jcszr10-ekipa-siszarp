namespace Manage_tasks_Biznes_Logic.Model
{
    /// <summary>
    /// Klasa obsługująca zadanie.
    /// </summary>
    public class ProjectTask
    {
        public Guid Id { get; set; } //do przyszłej obsługi bazy danych 

        //kolejne właściwości do dodania
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public Status? Status { get; set; }
        public DateTime? FinishDate { get; set; }
        public User? AssignedUser { get; set; }



        public ProjectTask()
        {
            Status = new Status();
        }
        /// <summary>
        /// Podstawowy konstruktor do tworzenia nowego zadania.
        /// </summary>
        /// <param name="TaskName">Parametr przedstawiający nazwę zadania.</param>
        /// <param name="TaskDescription">Parametr przedstawiający opis zadania.</param>        
        public ProjectTask(string TaskName, string TaskDescription) : this()
        {
            this.TaskName = TaskName;
            this.TaskDescription = TaskDescription;

            FinishDate = null;
            AssignedUser = null;
            Id = Guid.NewGuid();
        }
        public ProjectTask(Guid Id, string TaskName, string TaskDescription, Status Status, DateTime? FinishDate, User? AssignedUser)
        {
            this.TaskName = TaskName;
            this.TaskDescription = TaskDescription;
            this.Status.ChangeStatus(Status.StatusID());
            this.FinishDate = FinishDate;
            this.AssignedUser = AssignedUser;
            this.Id = Id;
            CheckIfFinished();
        }
        public void CheckIfFinished()
        {
            if (Status.StatusID() == 2)
            {
                FinishDate = DateTime.Now;
            }
            else
            {
                FinishDate = null;
            }
        }
        /// <summary>
        /// Metoda klasy zwracająca tablice z właściwościami.
        /// </summary>
        public string[] TaskDetails()
        {
            if (AssignedUser == null && FinishDate == null)
            {
                return new string[]
                {
                    TaskName, TaskDescription, Status.ShowCurrentStatus(), string.Empty, string.Empty
                };
            }
            else if (FinishDate == null)
            {
                return new string[]
                {
                    TaskName, TaskDescription, Status.ShowCurrentStatus(), string.Empty, AssignedUser.FirstName
                };
            }
            else if (AssignedUser == null)
            {
                return new string[]
                {
                    TaskName, TaskDescription, Status.ShowCurrentStatus(), FinishDate.ToString(), string.Empty
                };
            }
            else
            {
                return new string[]
                {
                    TaskName, TaskDescription, Status.ShowCurrentStatus(), FinishDate.ToString(), AssignedUser.FirstName
                };
            }
        }





    }
}
