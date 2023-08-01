namespace Manage_tasks
{
    public class Status
    {
        //Tablica ze zdefiniowaną listą statusów.
        private readonly string[] StatusList = { "ToDo", "InProgress", "Done" };
        public string CurrentStatus { get; set; }
        /// <summary>
        /// Konstruktor przypisujący status "ToDo".
        /// </summary>
        public Status() 
        {
            CurrentStatus = StatusList[0];
        }
        
        /// <summary>
        /// Metoda zwracająca aktualny status.
        /// </summary>
        /// <returns>Zwraca obecny status jako String.</returns>
        public string ShowCurrentStatus() 
        { 
            return CurrentStatus; 
        }
        /// <summary>
        /// Metoda zmianiająca obecny status.
        /// </summary>
        /// <param name="StatusAsNumber">Zmianna typu int32 określająca status jako numer od 0 do 2.</param>
        public void ChangeStatus(int StatusAsNumber)
        {
            CurrentStatus = StatusList[StatusAsNumber];
        }
        public int StatusID()
        {
            return Array.IndexOf(StatusList, CurrentStatus);
        }
    }
}
