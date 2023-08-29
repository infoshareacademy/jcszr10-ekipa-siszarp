using System.Diagnostics.CodeAnalysis;

namespace Manage_tasks_Biznes_Logic.Model;

public class Team
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public User? Leader { get; set; }

    public List<User> Members { get; set; } = new();
}
