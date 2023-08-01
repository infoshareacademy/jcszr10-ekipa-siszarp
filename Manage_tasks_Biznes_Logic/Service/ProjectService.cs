﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Manage_tasks;
using Manage_tasks_Biznes_Logic.Model;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
 

namespace Manage_tasks_Biznes_Logic.Service
{


    public class ProjectService

    {

        private List<Project> Projects = new List<Project>(); //Zapisywanie do jsone

        const string _nameJsonFile = "ListaProjectow.json";

        public void LoadProjectsFromJson()   
        {
            string objectJsonFie = File.ReadAllText(_nameJsonFile);
            Projects = JsonSerializer.Deserialize<List<Project>>(objectJsonFie);
        }
        public void SaveProjectToJson()  // именить название  - записиь файла json.
        {

            string objectSerialized = JsonSerializer.Serialize(Projects);
            File.WriteAllText("ListaProjectow.json", objectSerialized);
        }

        void AssignTeam(int index)
        {
            // Projects[index].ProjectTeam.
        }

        public List<Project> GetAllProject()
        {
            return Projects;
        }

        public Project GetProject(int projectId)  
        {
            try
            {
                return Projects[projectId];
            }
            catch (Exception ex)
            {
                Project ExceptionProject = new Project();
                ExceptionProject.Crash();
                return ExceptionProject;

            }
        }
        public void AssignTaskToProject(int index)
        {
            //Console.WriteLine("Podan neme");
            //string name = Console.ReadLine(); 
            //Projects[index].Tasks.AddTask(new ProjectTask());        
        }

        public string DisplayProjectDetails(int index)
        {

            try
            {

                if (Projects[index].ProjectTeam != null)
                {
                    return $@"Nazwa Projektu
{Projects[index].Name} 
Opis Projektu 
{Projects[index].Description}
Ekipa: {Projects[index].ProjectTeam.Name} Lider: {Projects[index].ProjectTeam.Leader}
";
                }
                return $@"Nazwa Projektu
{Projects[index].Name} 
Opis Projektu
{Projects[index].Description}";
            }
            catch (Exception ex)
            {
                return "Żaden projekt nie został wybrany lub stworzony";

            }

        }
        public void RemoveProject(int index)
        { 
             
            try
            {
                Projects.Remove(Projects[index]);
                SaveProjectToJson();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                 
            }
                                        // Zapisujemy nowa info do jsone po usunieciu projectu
        }

        public void CreateProject(string name, string description)
        {
            Projects.Add(new Project(name, description)); // tu zapisujemy do jsone
            SaveProjectToJson();
        }


    }


}
