using System.Diagnostics.CodeAnalysis;

namespace Manage_tasks;

internal class Team
{
    private static readonly UserEqualityComparer _userEquality = new();

    private readonly HashSet<User> _members = new(_userEquality);
    private User _leader;

    public int Id { get; private set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public User Leader
    {
        get
        {
            return _leader;
        }

        [MemberNotNull(nameof(_leader))]
        set
        {
            // Lider powinien być w zespole.
            _members.Add(value);

            _leader = value;
        }
    }

    public Team(IEnumerable<User> users, string name, User leader, string? description = null)
    {
        Name = name;
        Description = description;

        _members.UnionWith(users);

        Leader = leader;
    }

    public Team(string name, User leader, string? description = null)
    {
        Name = name;
        Description = description;

        Leader = leader;
    }

    public bool AddUser(User user)
    {
        return _members.Add(user);
    }

    public bool RemoveUser(User user)
    {
        // Nie można usunąć lidera zespołu z listy.
        if (_userEquality.Equals(user, Leader))
        {
            return false;
        }

        return _members.Remove(user);
    }

    public IEnumerable<User> GetMembers()
    {
        return _members;
    }

    public override string ToString()
    {
        return $"{Name} Lider: {Leader}";
    }

    public string ToStringVerbose()
    {
        string result = Name;

        if (Description is not null)
        {
            result += $" {Description}";
        }

        result += $"\nLider: {Leader}";
        result += "\nPozostali członkowie:";

        foreach (User user in _members)
        {
            if (_userEquality.Equals(user, Leader))
            {
                continue;
            }

            result += $"\n{user}";
        }

        return result;
    }

    // TODO: Jak będziemy mieć bazę danych to trzeba zamienić na sprawdzanie po Id.
    private class UserEqualityComparer : IEqualityComparer<User>
    {
        public bool Equals(User? x, User? y)
        {
            if (x is null || y is null)
            {
                return false;
            }

            return x.FirstName.Equals(y.FirstName) && x.LastName.Equals(y.LastName);
        }

        public int GetHashCode([DisallowNull] User obj)
        {
            HashCode hashCode = new();
            hashCode.Add(obj.FirstName);
            hashCode.Add(obj.LastName);

            return hashCode.ToHashCode();
        }
    }
}
