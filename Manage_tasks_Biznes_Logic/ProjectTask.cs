using Manage_tasks_Biznes_Logic.Model;
namespace Manage_tasks
{
    /// <summary>
    /// Klasa obsługująca zadanie.
    /// </summary>
    public class ProjectTask
    {
        public int Id { get; set; } //do przyszłej obsługi bazy danych 

        //kolejne właściwości do dodania
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public Status Status { get; set; }
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
        public ProjectTask(string TaskName, string TaskDescription) :this()
        {
            this.TaskName = TaskName;
            this.TaskDescription = TaskDescription;
            
            this.FinishDate = null;
            this.AssignedUser = null;
            
        }
        public ProjectTask(int Id, string TaskName, string TaskDescription, Status Status, DateTime? FinishDate, User? AssignedUser)
        {
            this.TaskName= TaskName;
            this.TaskDescription= TaskDescription;
            this.Status.ChangeStatus(Status.StatusID());
            this.FinishDate= FinishDate;
            this.AssignedUser = AssignedUser;
            this.Id = Id;
            CheckIfFinished();
        }
        public void CheckIfFinished()
        {
            if(this.Status.StatusID() == 2)
            {
                this.FinishDate = DateTime.Now;
            }
            else 
            {
                this.FinishDate = null; 
            }
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
            else if(this.FinishDate == null)
            {
                return new string[]
                {
                    this.TaskName, this.TaskDescription, this.Status.ShowCurrentStatus(), string.Empty, this.AssignedUser.FirstName
                };
            }
            else if(this.AssignedUser == null)
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
