﻿using System.Diagnostics.CodeAnalysis;

namespace Manage_tasks_Biznes_Logic.Model;

public class Team
{
    private readonly List<User> _members = new();
    private User _leader;

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

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
            if (!_members.Contains(value))
            {
                _members.Add(value);
            }

            _leader = value;
        }
    }

    public Team(string name, string description, User leader, IEnumerable<User> members)
    {
        Id = Guid.NewGuid();

        Name = name;
        Description = description;

        _members.AddRange(members);

        Leader = leader;
    }

    public Team(string name, string description, User leader)
    {
        Id = Guid.NewGuid();

        Name = name;
        Description = description;

        Leader = leader;
    }

    public bool AddUser(User user)
    {
        // Nie można dodawać takich samych użytkowników.
        if (_members.Contains(user))
        {
            return false;
        }

        _members.Add(user);

        return true;
    }

    public bool RemoveUser(User user)
    {
        // Nie można usunąć lidera zespołu.
        if (user == Leader)
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
            if (user == Leader)
            {
                continue;
            }

            result += $"\n{user}";
        }

        return result;
    }
}
