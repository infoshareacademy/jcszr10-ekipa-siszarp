using AutoMapper;
using Manage_tasks_Biznes_Logic.Dtos.Project;
using Manage_tasks_Biznes_Logic.Dtos.Team;
using Manage_tasks_Biznes_Logic.Model;
using Manage_tasks_Database.Context;
using Manage_tasks_Database.Entities;
using Microsoft.EntityFrameworkCore;
using TaskStatus = Manage_tasks_Database.TaskStatus;

namespace Manage_tasks_Biznes_Logic.Service;

public class ProjectService : IProjectService
{
    private readonly DataBaseContext _dbContext;
    private readonly ITasksListService _tasksListService;
    private readonly IMapper _mapper;
    public ProjectService(DataBaseContext dbContext, ITasksListService tasksListService, IMapper mapper)
    {
        _dbContext = dbContext;
        _tasksListService = tasksListService;
        _mapper = mapper;
    }

    public async Task<List<Project>> GetAllProjects()
    {
        var projectEntities = await _dbContext.ProjectEntities
            .Include(p => p.Team)
            .ThenInclude(t => t.Leader)
            .Include(p => p.Team)
            .ThenInclude(t => t.Members)
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
                .ThenInclude(t => t.Leader)
            .Include(p => p.TaskLists)
                .ThenInclude(t => t.Tasks)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (projectEntity == null)
        {
            return null;
        }

        var project = ConvertProjectEntity(projectEntity);
        return project;
    }

    public async Task<Guid> CreateProject(string name, string description, Guid teamId)
    {
        var teamEntity = await _dbContext.TeamEntities.FindAsync(teamId);

        if (teamEntity is null)
        {
            throw new InvalidOperationException("Team do not exist.");
        }

        var project = new ProjectEntity
        {
            Name = name,
            Description = description,
            Team = teamEntity
        };

        await _dbContext.ProjectEntities.AddAsync(project);
        await _tasksListService.CreateTasksList("Backlog", project.Id);
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
        //project.Team.Leader = newTeam.Leader;

        await _dbContext.SaveChangesAsync();
    }

    public async Task<ProjectListForUserDto> GetProjectListForUser(Guid userId)
    {
        var userProjects = await _dbContext.ProjectEntities
            .Include(p => p.Team)
                .ThenInclude(t => t.Members)
            .Include(p => p.TaskLists)
                .ThenInclude(tl => tl.Tasks)
            .Where(p => p.Team.Members.Any(m => m.Id == userId))
            .ToListAsync();

        var projectsOwner = _mapper.Map<IEnumerable<ProjectEntity>, List<ProjectBasicDto>>(userProjects
            .Where(p => p.Team.LeaderId == userId));

        var projectsMember = _mapper.Map<IEnumerable<ProjectEntity>, List<ProjectBasicDto>>(userProjects
            .ExceptBy(projectsOwner.Select(p => p.ProjectId), p => p.Id));

        SetProjectCompletionPercent(projectsOwner);
        SetProjectCompletionPercent(projectsMember);

        var dto = new ProjectListForUserDto
        {
            ProjectsOwner = projectsOwner,
            ProjectsMember = projectsMember
        };

        return dto;

        void SetProjectCompletionPercent(IEnumerable<ProjectBasicDto> projectDto)
        {
            foreach (var project in projectDto)
            {
                var completionPercent = GetProjectCompletionPercent(userProjects.First(p => p.Id == project.ProjectId));

                project.CompletionPercent = completionPercent;
            }
        }
    }

    private static int GetProjectCompletionPercent(ProjectEntity project)
    {
        var tasks = project.TaskLists.SelectMany(tl => tl.Tasks).ToList();

        if (tasks.Count == 0)
        {
            return 100;
        }

        var completedTasksCount = tasks.Count(t => t.StatusId == TaskStatus.Done);

        var completionPercent = (float)completedTasksCount / tasks.Count * 100.0f;

        return (int)completionPercent;
    }

    private Project ConvertProjectEntity(ProjectEntity projectEntity)
    {
        Team team = null!;

        if (projectEntity.Team is not null)
        {
            team = new Team
            {
                Id = projectEntity.Team.Id,
                Name = projectEntity.Team.Name,
                Description = projectEntity.Team.Description ?? string.Empty,
                Leader = new User
                {
                    Id = projectEntity.Team.Leader.Id,
                    FirstName = projectEntity.Team.Leader.FirstName,
                    LastName = projectEntity.Team.Leader.LastName,
                },
            };
        }

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