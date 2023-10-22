using Manage_tasks_Biznes_Logic.Model;
using Manage_tasks_Database.Context;
using Manage_tasks_Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;



namespace Manage_tasks_Biznes_Logic.Service
{
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
                .Include(p => p.Teams)
                .Include(p => p.TaskLists)
                .ToListAsync();
            var projects = projectEntities.Select(ConvertProjectEntity).ToList();
            return projects;
        }
        public async Task<Project?> GetProjectById(Guid id)
        {
            var projectEntity = await _dbContext.ProjectEntities
                .Include(p => p.Teams)
                .Include(p => p.TaskLists).ThenInclude(t => t.Tasks)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (projectEntity == null)
            {
                return null;
            }
            var project = ConvertProjectEntity(projectEntity);
            return project;
        }
        public async Task<Guid> CreateProject (string name, string description)
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
            if(project is null)
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
        public async Task AddTeamToProject(Guid projectId, IEnumerable<Guid> newTeamsIds)
        {
            var project = await _dbContext.ProjectEntities
                .Include(p => p.Teams)
                .FirstOrDefaultAsync(p =>p.Id == projectId);
            if (project is null)
            {
                return;
            }
            var possibleTeams = newTeamsIds.Except(project.Teams.Select(t => t.Id));

            var teamEntities = await _dbContext.TeamEntities.ToListAsync();

            var teamsToAdd = teamEntities.IntersectBy(possibleTeams, t => t.Id).ToList();

            foreach (var team in teamsToAdd)
            {
                project.Teams.Add(team);
            }

            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteTeamFromProject(Guid projectId, Guid teamIdToDelete)
        {
            var project = await _dbContext.ProjectEntities
                .Include(p => p.Teams)
                .FirstOrDefaultAsync(p => p.Id == projectId);
            if (project is null)
            {
                return;
            }
            var teamToRemove = project.Teams.FirstOrDefault(t => t.Id == teamIdToDelete);
            if(teamToRemove is null)
            {
                return;
            }
            project.Teams.Remove(teamToRemove);
            await _dbContext.SaveChangesAsync();
        }
        private  Project ConvertProjectEntity(ProjectEntity projectEntity)
        {
            var teams = projectEntity.Teams
                .Select(u => new Team
                {
                    Id = u.Id,
                    Name = u.Name,
                    Description = u.Description ?? string.Empty
                }).ToList();
            
            var project = new Project
            {
                Id = projectEntity.Id,
                Name = projectEntity.Name,
                Description = projectEntity.Description ?? string.Empty,
                ProjectTeams = teams,
                Tasks = projectEntity.TaskLists.Select(_tasksListService.EntityToModel).ToList()    
            };

            return project;
        }

        //private static List<Project> Projects = new();

        //const string _nameJsonFile = "ListaProjectow.json";

        //public void SaveProjectToJson()
        //{

        //    string objectSerialized = JsonSerializer.Serialize(Projects);
        //    File.WriteAllText(_nameJsonFile, objectSerialized);
        //}

        //public List<Project> LoadProjectsFromJson()
        //{
        //    if (!File.Exists(_nameJsonFile))
        //    {
        //        Projects = new List<Project>();
        //    }

        //    Projects = JsonSerializer.Deserialize<List<Project>>(File.ReadAllText(_nameJsonFile));

        //    return Projects;
        //}

        //public List<Project> GetAllProjects()
        //{
        //    return LoadProjectsFromJson();
        //}

        //public Project GetProjectById(Guid projectId)
        //{
        //    try
        //    {
        //        return GetAllProjects().FirstOrDefault(p => p.Id == projectId);
        //    }
        //    catch (Exception ex)
        //    {
        //        Project ExceptionProject = new Project();
        //        ExceptionProject.Crash();
        //        return ExceptionProject;

        //    }
        //}

        //public void RemoveProject(Guid id)
        //{
        //    var projects = GetAllProjects();

        //    var projectInDatabase = projects.FirstOrDefault(p => p.Id == id);

        //    if (projectInDatabase is null)
        //    {
        //        return;
        //    }

        //    projects.Remove(projectInDatabase);

        //    SaveProjectToJson();
        //}

        //public void CreateProject(string name, string description)
        //{
        //    Projects.Add(new Project(name, description));
        //    SaveProjectToJson();
        //}

        //public void UpdateProject(Project project)
        //{
        //    var projects = GetAllProjects();

        //    var projectInDatabase = projects.FirstOrDefault(u => u.Id == project.Id);

        //    if (projectInDatabase is not null)
        //    {
        //        projects.Remove(projectInDatabase);
        //    }
        //    else
        //    {
        //        project.Id = Guid.NewGuid();
        //    }

        //    projects.Add(project);

        //    SaveProjectToJson();
        //}
        //public void EditNameAndDescription(Guid projectId, string newName, string newDescription)
        //{
        //    var projects = GetAllProjects();
        //    var projectInDataBase = GetProjectById(projectId);

        //    if (projectInDataBase is null)
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        projectInDataBase.Name = newName;
        //        projectInDataBase.Description = newDescription;
        //    }

        //    projects.Add(projectInDataBase);
        //    SaveProjectToJson();
        //}
        //public void ChangeTeam(Guid projectId, Guid newProjectTeamId)
        //{
        //    var projects = GetAllProjects();
        //    var projectInDataBase = GetProjectById(projectId);
        //    if (projectInDataBase is null)
        //    {
        //        projectInDataBase.Id = new Guid();
        //        projectInDataBase.ProjectTeamId = newProjectTeamId;
        //    }
        //    else
        //    {
        //        projectInDataBase.ProjectTeamId = newProjectTeamId;
        //    }
        //    projects.Add(projectInDataBase);
        //    SaveProjectToJson();
        //}
    }
}
