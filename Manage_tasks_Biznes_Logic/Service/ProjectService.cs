using System;
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
            if (!File.Exists(_nameJsonFile))
            {
                Projects = new List<Project>();//🤨
                return;
            }

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

        public void RemoveProject(int index)
        { 
             
            try
            {
                //Projects.Remove(Projects[index]);
                Projects.RemoveAt(index);
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
