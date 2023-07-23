using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Manage_tasks;
using Manage_tasks_Biznes_Logic.Model;

namespace Manage_tasks_Biznes_Logic.Service
{
    

    public class ProjectService   
   
    {
        private List<Project> Projects = new List<Project>(); //Zapisywanie do jsone
        

       
        void AssignTeam(int index)
        {
          // Projects[index].ProjectTeam.
        }

        public List<Project> GetAllProject()
        {
            return Projects;
        }

        public Project GetProject(int projectId)  // pobieranie z jsone
        {                               
            try
            {
                return Projects[projectId];
            }
            catch( Exception ex )
            {
                Project ExceptionProject = new Project();
                ExceptionProject.Name = "Project nie został wybrany";
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

                if(Projects[index].ProjectTeam != null)
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
            Projects.Remove(Projects[index]);
        }

        public void CreateProject(string name, string description)
        {
            Projects.Add(new Project(name, description)); // tu zapisujemy do jsone
        }

       
    }


}
