using Manage_tasks_Biznes_Logic.Model;

namespace Manage_tasks_Biznes_Logic.Service;

public interface IUserService
{
    User? GetUserById(Guid id);
    void UpdateUser(User user);
    void DeleteUser(Guid id);
    List<User> GetAllUsers();
}