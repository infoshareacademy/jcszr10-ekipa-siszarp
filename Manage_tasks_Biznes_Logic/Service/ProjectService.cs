using Manage_tasks_Biznes_Logic.Dtos.Team;
using Manage_tasks_Biznes_Logic.Model;
using Manage_tasks_Database.Context;
using Manage_tasks_Database.Entities;
using Microsoft.EntityFrameworkCore;



namespace Manage_tasks_Biznes_Logic.Service;

public class ProjectService : IProjectService
{
    private readonly DataBaseContext _dbContext;
    private readonly ITasksListService _tasksListService;
    private readonly ITeamService _teamService;
    public ProjectService(DataBaseContext dbContext, ITasksListService tasksListService, ITeamService teamService)
    {
        _dbContext = dbContext;
        _tasksListService = tasksListService;
        _teamService = teamService;
    }
    public async Task<List<Project>> GetAllProjects()
    {
        var projectEntities = await _dbContext.ProjectEntities
            .Include(p => p.Team)
            .Include(p => p.TaskLists)
            .ToListAsync();
        var projects = projectEntities.Select(p => new Project
        {
            Name = p.Name,
            Description = p.Description,
            ProjectTeam = new Team
            {
                Id = p.Team.Id,
                Name = p.Team.Name,
                Description = p.Team.Description
            },
            Tasks = new List<TasksList> ()
        }).ToList();
        return projects;
    }
    public async Task<Project?> GetProjectById(Guid id)
    {
        var projectEntity = await _dbContext.ProjectEntities
            .Include(p => p.Team)
            .Include(p => p.TaskLists).ThenInclude(t => t.Tasks)
            .FirstOrDefaultAsync(p => p.Id == id);

        var teamEntity = await _dbContext.TeamEntities
            .Include(t => t.Leader)
            .Include(t => t.Members)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (projectEntity == null || teamEntity == null)
        {
            return null;
        }

        var project = ConvertProjectEntity(projectEntity, teamEntity);
        return project;
    }
    public async Task<Guid> CreateProject(string name, string description)
    {
        var projectId = Guid.NewGuid();
        var project = new ProjectEntity
        {
            Id = projectId,
            Name = name,
            Description = description,

        };
        await _dbContext.ProjectEntities.AddAsync(project);
        await _tasksListService.CreateTasksList("Backlog", projectId);
        await _dbContext.SaveChangesAsync();
        return project.Id;
    }
    public async Task RemoveProject(Guid id)
    {
        var project = await _dbContext.ProjectEntities.FindAsync(id);
        if (project is null)
        {
            return;
        }
        _dbContext.ProjectEntities.Remove(project);
        await _dbContext.SaveChangesAsync();
    }
    public async Task EditNameAndDescription(Guid projectId, string newName, string newDescription)
    {
        var project = await _dbContext.ProjectEntities.FirstOrDefaultAsync(p => p.Id == projectId);
        if (project == null)
        {
            return;
        }
        project.Name = newName;
        project.Description = newDescription;
        await _dbContext.SaveChangesAsync();
    }
    //public async Task<ICollection<Team>> GetAvailableProjectTeams(Guid projectId)
    //{
    //    var availableProjectTeams = await _dbContext.TeamEntities
    //        .Where(u => u.Projects.Any(t => t.Id == projectId))
    //        .ToListAsync();

    //    return (ICollection<Team>)availableProjectTeams;
    //}
    public async Task ChangeProjectTeam(Guid projectId, Guid newTeamId)
    {
        var project = await _dbContext.ProjectEntities
            .Include(p => p.Team)
            .FirstOrDefaultAsync(p => p.Id == projectId);

        if (project is null || project.TeamId == newTeamId)
        {
            return;
        }

        var newTeam = await _dbContext.TeamEntities.FindAsync(newTeamId);

        if (newTeam is null)
        {
            return;
        }

        project.Team = newTeam;
        project.TeamId = newTeam.Id;
        project.Team.Leader = newTeam.Leader;

        await _dbContext.SaveChangesAsync();
    }

     //public async Task DeleteTeamFromProject(Guid projectId, Guid teamIdToDelete)
     //   {
     //       var project = await _dbContext.ProjectEntities
     //           .Include(p => p.Teams)
     //           .FirstOrDefaultAsync(p => p.Id == projectId);
     //       if (project is null)
     //       {
     //           return;
     //       }
     //       var teamToRemove = project.Teams.FirstOrDefault(t => t.Id == teamIdToDelete);
     //       if(teamToRemove is null)
     //       {
     //           return;
     //       }
     //       project.Teams.Remove(teamToRemove);
     //       await _dbContext.SaveChangesAsync();
     //   }

    private Project ConvertProjectEntity(ProjectEntity projectEntity, TeamEntity teamEntity)
    {
        var team = new Team
        {
            Id = teamEntity.Id,
            Name = teamEntity.Name,
            Description = teamEntity.Description ?? string.Empty,
            Leader = new User
            {
                Id = teamEntity.Leader.Id,
                FirstName = teamEntity.Leader.FirstName,
                LastName = teamEntity.Leader.LastName,
            },
        };
        var project = new Project
        {
            Id = projectEntity.Id,
            Name = projectEntity.Name,
            Description = projectEntity.Description ?? string.Empty,
            ProjectTeam = team,
            Tasks = projectEntity.TaskLists.Select(_tasksListService.EntityToModel).ToList()
        };
        return project;
    }
}

