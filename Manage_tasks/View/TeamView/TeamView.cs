using Manage_tasks.Model;
using Manage_tasks_Biznes_Logic.Data;
using Manage_tasks_Biznes_Logic.Model;

namespace Manage_tasks.View.TeamView;

public class TeamView
{
    private readonly Team _team;

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
                    Data.TeamService.DeleteTeam(_team);
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

        _team.Name = newTeamName;

        Data.TeamService.UpdateTeam(_team);
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

        _team.Description = newTeamDescription;

        Data.TeamService.UpdateTeam(_team);
    }

    public void ChangeLeader()
    {
        var prompt = "Edycja lidera zespołu";

        var availabeLeaders = _team.GetMembers().Where(user => user != _team.Leader).ToList();

        if (availabeLeaders.Count == 0)
        {
            return;
        }

        var options = availabeLeaders.Select(user => user.ToString()).ToArray();

        var optionIndex = new ManageMenu(prompt, options).Run();

        _team.Leader = availabeLeaders[optionIndex];

        Data.TeamService.UpdateTeam(_team);
    }

    public void AddUser()
    {
        var prompt = "Dodaj nowego członka zespołu";

        //var availabeMembers = Data.UserService.GetAllUsers().Except(_team.GetMembers()).ToList();
        var availabeMembers = Data.UserService.GetAllUsers().Where(u1 => _team.GetMembers().All(u2 => u2.Id != u1.Id)).ToList();

        if (availabeMembers.Count == 0)
        {
            return;
        }

        var options = availabeMembers.Select(user => user.ToString()).ToArray();

        var optionIndex = new ManageMenu(prompt, options).Run();

        if (!_team.AddUser(availabeMembers[optionIndex]))
        {
            return;
        }

        Data.TeamService.UpdateTeam(_team);
    }

    public void RemoveUser()
    {
        var prompt = "Usuń aktualnego członka zespołu";

        var members = _team.GetMembers().Where(user => user != _team.Leader).ToList();

        if (members.Count == 0)
        {
            return;
        }

        var options = members.Select(user => user.ToString()).ToArray();

        var optionIndex = new ManageMenu(prompt, options).Run();

        if (!_team.RemoveUser(members[optionIndex]))
        {
            return;
        }

        Data.TeamService.UpdateTeam(_team);
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

        foreach (var user in _team.GetMembers())
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
