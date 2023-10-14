using Manage_tasks_Biznes_Logic.Service;
namespace Manage_tasks_Biznes_Logic.Data
{
    public class Data
    {
        public static ProjectService projectService = new ProjectService();

        public static readonly UserService UserService = new(null);

        public static readonly TeamService TeamService = new(null);
    }


}
