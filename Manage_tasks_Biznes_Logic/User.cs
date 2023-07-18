namespace Manage_tasks;

public class User
{
    public int Id { get; private set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    // Czy stanowisko powinno być oddzielną klasą?
    public string Position { get; set; }

    public User(string firstName, string lastName, string position)
    {
        FirstName = firstName;
        LastName = lastName;
        Position = position;
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName} Stanowisko: {Position}";
    }
}
