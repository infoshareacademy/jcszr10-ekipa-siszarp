﻿namespace Manage_tasks_Biznes_Logic.Model
{

    public class Project

    {

        public string Name { get; set; }
        public string Description { get; set; }
        public List<Team> ProjectTeams { get; set; }
        public Guid ProjectTeamId { get; set; }
        public Guid? OwnerId { get; set; }
        public List<TasksList> Tasks { get; set; }

        public int OutID { get; set; }
        public Guid Id { get; set; }

        public Project()
        {
            


        }

        public Project(string projectName, string projectDescription) : this()
        {
            Name = projectName;
            Description = projectDescription;
            Id = Guid.NewGuid();
        }


        public void AddTeams(List<Team> teams)
        {
            ProjectTeams = teams;
            ProjectTeamId = Guid.NewGuid();
        }

        public void Crash()
        {
            Name = "Error";
            Description = "Error";
        }
    }
}
