using Manage_tasks_Biznes_Logic.Model;

namespace Manage_tasks_Biznes_Logic.Service;

public class UserService
{
    private readonly List<User> _users = new();

    public User? GetUserById(Guid id)
    {
        throw new NotImplementedException();
    }

    public void UpdateUser(User user)
    {
        var userInDatabase = _users.Where(u => u.Id == user.Id).FirstOrDefault();

        if (userInDatabase is null)
        {
            _users.Add(user);
            return;
        }
    }

    public void DeleteUser(User user)
    {
        var userInDatabase = _users.Where(u => u.Id == user.Id).FirstOrDefault();

        if (userInDatabase is not null)
        {
            _users.Remove(userInDatabase);
            return;
        }
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _users;
    }
}