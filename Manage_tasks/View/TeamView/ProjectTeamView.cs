using Manage_tasks.Model;
using Manage_tasks_Biznes_Logic.Data;
using Manage_tasks_Biznes_Logic.Model;
using System.IO;

namespace Manage_tasks.View.TeamView;

public class ProjectTeamView
{
    private readonly Project _project;

    public ProjectTeamView(Project project)
    {
        _project = project;
    }

    public void Run()
    {
        bool goBack = false;

        while (!goBack)
        {
            if (_project.ProjectTeam is not null)
            {
                goBack = RunTeamAssigned();
                continue;
            }

            goBack = RunEmptyTeam();
        }
    }

    private bool RunTeamAssigned()
    {
        var options = new string[] { "Zmień zespół", "Usuń zespół", "Wstecz" };

        var goBackIndex = options.Length - 1;

        var optionIndex = new ManageMenu(GetTeamAssignedPrompt(), options).Run();

        switch (optionIndex)
        {
            case 0:
                ChangeTeam();
                break;

            case 1:
                _project.AddTeam(null);
                // TODO zaktualizować bazę danych.
                break;

            default:
                break;
        }

        if (optionIndex == goBackIndex)
        {
            return true;
        }

        return false;
    }

    private void ChangeTeam()
    {
        var prompt = "Zmień zespół";

        var availabeTeams = Data.TeamService.GetAllTeams().Where(team => team.Id != _project.ProjectTeam.Id).ToList();

        if (availabeTeams.Count == 0)
        {
            return;
        }

        var options = availabeTeams.Select(team => team.ToString()).ToArray();

        var optionIndex = new ManageMenu(prompt, options).Run();

        _project.AddTeam(availabeTeams[optionIndex]);

        // TODO zaktualizować bazę danych.
    }

    private bool RunEmptyTeam()
    {
        var options = new string[] { "Przypisz zespół", "Wstecz" };

        var optionIndex = new ManageMenu("Zespół nie został przypisany", options).Run();

        if (optionIndex == 0)
        {
            AssignTeam();

            return false;
        }

        return true;
    }

    private void AssignTeam()
    {
        var prompt = "Przypisz zespół";

        var availabeTeams = Data.TeamService.GetAllTeams();

        if (availabeTeams.Count == 0)
        {
            return;
        }

        var options = availabeTeams.Select(team => team.ToString()).ToArray();

        var optionIndex = new ManageMenu(prompt, options).Run();

        _project.AddTeam(availabeTeams[optionIndex]);
    }

    private string GetTeamAssignedPrompt()
    {
        var prompt = string.Concat(
             $"Przypisany zespół {_project.ProjectTeam.Name}", Environment.NewLine, Environment.NewLine,
             $"Opis {_project.ProjectTeam.Description}", Environment.NewLine, Environment.NewLine,
             $"Lider {_project.ProjectTeam.Leader}", Environment.NewLine, Environment.NewLine,
             "Członkowie zespołu:", Environment.NewLine, Environment.NewLine,
             GetMembersString());

        return prompt;
    }

    private string GetMembersString()
    {
        string result = string.Empty;

        foreach (var user in _project.ProjectTeam.GetMembers())
        {
            result = string.Concat(result, user.ToString(), Environment.NewLine);
        }

        return result;
    }
}
