namespace Manage_tasks_Biznes_Logic.Model;

public class User
{
    public int Id { get; set; }

    public string Username { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    // Czy stanowisko powinno być oddzielną klasą?
    public string Position { get; set; }

    public User(string username, string firstName, string lastName, string position)
    {
        Username = username;
        FirstName = firstName;
        LastName = lastName;
        Position = position;
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName} Stanowisko: {Position}";
    }
}
