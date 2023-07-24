using Manage_tasks.Model;
using Manage_tasks.View.UserView;
using Manage_tasks_Biznes_Logic.Data;
using Manage_tasks_Biznes_Logic.Model;

namespace Manage_tasks.View.TeamView;

public class TeamsListView
{
    private readonly List<Team> _teams = new();

    private readonly string[] _staticOptions = { "Dodaj nowy zespół", "Wstecz" };

    private string[] _allOptions;

    private int _goBackIndex;
    private int _addUserIndex;

    public void Run()
    {
        var prompt = "Lista zespołów";

        int optionIndex = int.MaxValue;
        _goBackIndex = int.MinValue;

        while (optionIndex != _goBackIndex)
        {
            UpdateOptions();

            optionIndex = new ManageMenu(prompt, _allOptions).Run();

            if (optionIndex == _addUserIndex)
            {
                var addTeamView = new AddTeamView();
                addTeamView.Run();
            }
            else if (optionIndex < _teams.Count)
            {
                var teamView = new TeamView(_teams[optionIndex]);
                teamView.Run();
            }
        }
    }

    public void UpdateOptions()
    {
        _teams.Clear();
        _teams.AddRange(Data.TeamService.GetAllTeams());

        var options = new List<string>();

        if (_teams.Count > 0)
        {
            options.AddRange(_teams.Select(team => team.ToString()));
        }

        options.AddRange(_staticOptions);

        _goBackIndex = options.Count - 1;
        _addUserIndex = options.Count - 2;

        _allOptions = options.ToArray();
    }
}
