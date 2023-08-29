using Manage_tasks.Model;
using Manage_tasks_Biznes_Logic.Data;
using Manage_tasks_Biznes_Logic.Model;

namespace Manage_tasks.View.TeamView;

public class AddTeamView
{
    public void Run()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.DarkYellow;

        DrawMessageAtCenter("Dodawanie nowego zespołu");
        Console.WriteLine();

        DrawMessageAtCenter("Podaj nazwę zespołu");
        var teamName = Console.ReadLine();

        DrawMessageAtCenter("Podaj opis zespołu");
        var teamDescription = Console.ReadLine();

        var teamMembers = GetMembers();

        var leader = GetLeader(teamMembers);

        var newTeam = new Team
        {
            Name = teamName,
            Description = teamDescription,
            Leader = leader,
            Members = teamMembers
        };

        var teamId = Data.TeamService.CreateTeam(newTeam.Name, newTeam.Description);

        Data.TeamService.AddMembersToTeam(teamId, newTeam.Members.Select(m => m.Id));

        Data.TeamService.ChangeTeamLeader(teamId, newTeam.Leader.Id);
    }

    private List<User> GetMembers()
    {
        var members = new List<User>();

        var availableUsers = Data.UserService.GetAllUsers();

        int optionIndex = int.MaxValue;
        int goBackIndex = int.MinValue;

        while (optionIndex != goBackIndex)
        {
            var prompt = string.Concat("Wybieranie członków zespołu", Environment.NewLine, GetSelectedMembersString(members));

            var options = new List<string>();
            options.AddRange(availableUsers.Select(user => user.ToString()));

            if (members.Count > 0)
            {
                options.Add("Zakończ wybieranie");

                goBackIndex = options.Count - 1;
            }

            optionIndex = new ManageMenu(prompt, options.ToArray()).Run();

            if (optionIndex < availableUsers.Count)
            {
                var selectedUser = availableUsers[optionIndex];

                members.Add(selectedUser);
                availableUsers.Remove(selectedUser);
            }
        }

        return members;
    }

    private User GetLeader(List<User> members)
    {
        var prompt = "Wybieranie lidera zespołu";

        var options = members.Select(user => user.ToString()).ToArray();

        var optionIndex = new ManageMenu(prompt, options).Run();

        return members[optionIndex];
    }

    private string GetSelectedMembersString(List<User> selectedMembers)
    {
        string result = string.Concat("Wybrani członkowie", Environment.NewLine);

        foreach (var user in selectedMembers)
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
