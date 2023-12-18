using Manage_tasks.Model;
using Manage_tasks_Biznes_Logic.Data;
using Manage_tasks_Biznes_Logic.Model;

namespace Manage_tasks.View.TeamView;

public class TeamView
{
    private Team _team;

    public TeamView(Team team)
    {
        _team = team;
    }

    public void Run()
    {
        var options = new string[] { "Edytuj nazwę", "Edytuj opis", "Zmień Lidera", "Dodaj członka", "Usuń członka", "Usuń zespół", "Wstecz" };

        var goBackIndex = options.Length - 1;

        int optionIndex = int.MaxValue;

        while (optionIndex != goBackIndex)
        {
            optionIndex = new ManageMenu(GetMainPrompt(), options).Run();

            switch (optionIndex)
            {
                case 0:
                    EditName();
                    break;

                case 1:
                    EditDescription();
                    break;

                case 2:
                    ChangeLeader();
                    break;

                case 3:
                    AddUser();
                    break;

                case 4:
                    RemoveUser();
                    break;

                case 5:
                    Data.TeamService.DeleteTeam(_team.Id);
                    optionIndex = goBackIndex;

                    break;

                default:
                    break;
            }
        }
    }

    public void EditName()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.DarkYellow;

        DrawMessageAtCenter("Edycja nazwy zespołu");
        Console.WriteLine();

        DrawMessageAtCenter($"Aktualna nazwa: {_team.Name}");
        Console.WriteLine();

        DrawMessageAtCenter("Podaj nową nazwę zespołu");
        var newTeamName = Console.ReadLine();

        Console.ResetColor();

        _team = new Team
        {
            Id = _team.Id,
            Name = newTeamName,
            Description = _team.Description,
            Leader = _team.Leader,
            Members = _team.Members,
        };

        Data.TeamService.EditNameAndDescription(_team.Id, _team.Name, _team.Description);
    }

    public void EditDescription()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.DarkYellow;

        DrawMessageAtCenter("Edycja opisu zespołu");
        Console.WriteLine();

        DrawMessageAtCenter($"Aktualny opis: {_team.Description}");
        Console.WriteLine();

        DrawMessageAtCenter("Podaj nowy opis zespołu");
        var newTeamDescription = Console.ReadLine();

        Console.ResetColor();

        _team = new Team
        {
            Id = _team.Id,
            Name = _team.Name,
            Description = newTeamDescription,
            Leader = _team.Leader,
            Members = _team.Members,
        };

        Data.TeamService.EditNameAndDescription(_team.Id, _team.Name, _team.Description);
    }

    public void ChangeLeader()
    {
        var prompt = "Edycja lidera zespołu";

        var availabeLeaders = _team.Members.Where(user => user != _team.Leader).ToList();

        if (availabeLeaders.Count == 0)
        {
            return;
        }

        var options = availabeLeaders.Select(user => user.ToString()).ToArray();

        var optionIndex = new ManageMenu(prompt, options).Run();

        _team = new Team
        {
            Id = _team.Id,
            Name = _team.Name,
            Description = _team.Description,
            Leader = availabeLeaders[optionIndex],
            Members = _team.Members,
        };

        Data.TeamService.ChangeTeamLeader(_team.Id, _team.Leader.Id);
    }

    public void AddUser()
    {
        var prompt = "Dodaj nowego członka zespołu";

        //var availabeMembers = Data.UserService.GetAllUsers().Except(_team.GetMembers()).ToList();
        var availabeMembers = Data.UserService.GetAllUsers().Where(u1 => _team.Members.All(u2 => u2.Id != u1.Id)).ToList();

        if (availabeMembers.Count == 0)
        {
            return;
        }

        var options = availabeMembers.Select(user => user.ToString()).ToArray();

        var optionIndex = new ManageMenu(prompt, options).Run();

        _team.Members.Add(availabeMembers[optionIndex]);

        Data.TeamService.AddMembersToTeam(_team.Id, new List<Guid> { availabeMembers[optionIndex].Id });
    }

    public void RemoveUser()
    {
        var prompt = "Usuń aktualnego członka zespołu";

        var members = _team.Members.Where(user => user != _team.Leader).ToList();

        if (members.Count == 0)
        {
            return;
        }

        var options = members.Select(user => user.ToString()).ToArray();

        var optionIndex = new ManageMenu(prompt, options).Run();

        if (_team.Leader == members[optionIndex])
        {
            return;
        }

        _team.Members.Remove(members[optionIndex]);

        Data.TeamService.DeleteMemberFromTeam(_team.Id, members[optionIndex].Id);
    }

    private string GetMainPrompt()
    {
        var prompt = string.Concat(
             $"Zespół {_team.Name}", Environment.NewLine, Environment.NewLine,
             $"Opis {_team.Description}", Environment.NewLine, Environment.NewLine,
             $"Lider {_team.Leader}", Environment.NewLine, Environment.NewLine,
             "Członkowie zespołu:", Environment.NewLine, Environment.NewLine,
             GetMembersString());

        return prompt;
    }

    private string GetMembersString()
    {
        string result = string.Empty;

        foreach (var user in _team.Members)
        {
            result = string.Concat(result, user.ToString(), Environment.NewLine);
        }

        return result;
    }

    private void DrawMessageAtCenter(string message)
    {
        var cursorLeft = (Console.WindowWidth - message.Length) / 2;

        Console.CursorLeft = cursorLeft;

        Console.WriteLine(message);

        Console.CursorLeft = cursorLeft;
    }
}
