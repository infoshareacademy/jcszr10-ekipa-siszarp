namespace Manage_tasks_Biznes_Logic.Model;

public class User
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Position { get; set; }

    public override string ToString()
    {
        return $"{FirstName} {LastName} Stanowisko: {Position}";
    }
}
