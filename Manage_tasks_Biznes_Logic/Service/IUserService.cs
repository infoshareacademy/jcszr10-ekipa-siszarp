using Manage_tasks_Biznes_Logic.Model;

namespace Manage_tasks_Biznes_Logic.Service;

public interface IUserService
{
    void SaveNewUser(User newUser);

    void UpdateUser(User user);

    User? GetUserById(int id);

    User? GetUserByUsername(string username);
}
