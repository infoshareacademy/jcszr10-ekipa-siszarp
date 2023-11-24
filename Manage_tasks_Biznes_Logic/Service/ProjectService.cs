using AutoMapper.Execution;
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
    public ProjectService(DataBaseContext dbContext, ITasksListService tasksListService)
    {
        _dbContext = dbContext;
        _tasksListService = tasksListService;
    }
    public async Task<List<Project>> GetAllProjects()
    {
        var projectEntities = await _dbContext.ProjectEntities
            .Include(p => p.Team)
            .Include(p => p.TaskLists)
            .ToListAsync();

        var projects = projectEntities
            .Select(ConvertProjectEntity)
            .ToList();

		return projects;
    }
    public async Task<Project?> GetProjectById(Guid id)
    {
        var projectEntity = await _dbContext.ProjectEntities
            .Include(p => p.Team)
            .Include(p => p.TaskLists).ThenInclude(t => t.Tasks)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (projectEntity == null)
        {
            return null;
        }

        var project = ConvertProjectEntity(projectEntity);
        return project;
    }
    public async Task<Guid> CreateProject(string name, string description, Guid ownerId)
    {
        var projectId = Guid.NewGuid();
        var project = new ProjectEntity
        {
            Id = projectId,
            Name = name,
            Description = description,
            OwnerId = ownerId
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
    //public async Task AddTeamToProject(Guid projectId, IEnumerable<Guid> newTeamsIds)
    //{
    //    var project = await _dbContext.ProjectEntities
    //        .Include(p => p.Teams)
    //        .FirstOrDefaultAsync(p => p.Id == projectId);
    //    if (project is null)
    //    {
    //        return;
    //    }
    //    var possibleTeams = newTeamsIds.Except(project.Teams.Select(t => t.Id));

    //    var teamEntities = await _dbContext.TeamEntities.ToListAsync();

    //    var teamsToAdd = teamEntities.IntersectBy(possibleTeams, t => t.Id).ToList();

    //    foreach (var team in teamsToAdd)
    //    {
    //        project.Teams.Add(team);
    //    }
    //}
        public async Task<List<Team>> GetAvailableProjectTeams(Guid projectId)
    {
        var availableProjectTeams = await _dbContext.TeamEntities
            .Where(u => u.Projects.Any(t => t.Id == projectId))
            .ToListAsync();

        var availableTeams = availableProjectTeams.Select(t => new Team
        {
                Id = t.Id,
                Name = t.Name,
                //Leader = t.Leader,
        }).ToList();

		return availableTeams;
	}
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
    private Project ConvertProjectEntity(ProjectEntity projectEntity)
    {
        Team team = null!;

        //if(projectEntity.Team is not null)
        //{
        //    team = new Team
        //    {
        //        Id = projectEntity.Team.Id,
        //        Name = projectEntity.Team.Name,
        //        Description = projectEntity.Team.Description ?? string.Empty,
        //        Leader = new User
        //        {
        //            Id = projectEntity.Team.Leader.Id,
        //            FirstName = projectEntity.Team.Leader.FirstName,
        //            LastName = projectEntity.Team.Leader.LastName,
        //        },
        //    };
        //}

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



