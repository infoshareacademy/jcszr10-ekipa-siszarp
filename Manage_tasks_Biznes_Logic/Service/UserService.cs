using Manage_tasks_Biznes_Logic.Model;
using System.Text.Json;

namespace Manage_tasks_Biznes_Logic.Service;

public class UserService : IUserService
{
    private const string UsersFileName = "Users.json";

    public User? GetUserById(Guid id)
    {
        return GetAllUsers().FirstOrDefault(user => user.Id == id);
    }

    public void UpdateUser(User user)
    {
        var users = GetAllUsers();

        var userInDatabase = users.FirstOrDefault(u => u.Id == user.Id);

        if (userInDatabase is not null)
        {
            users.Remove(userInDatabase);
        }
        else
        {
            user.Id = Guid.NewGuid();
        }

        users.Add(user);

        SaveUsers(users);
    }

    public void DeleteUser(Guid id)
    {
        var users = GetAllUsers();

        var userInDatabase = users.FirstOrDefault(u => u.Id == id);

        if (userInDatabase is null)
        {
            return;
        }

        users.Remove(userInDatabase);

        SaveUsers(users);
    }

    public List<User> GetAllUsers()
    {
        List<User>? users = null;

        if (File.Exists(UsersFileName))
        {
            users = JsonSerializer.Deserialize<List<User>>(File.ReadAllText(UsersFileName));
        }

        return users ?? new List<User>();
    }

    private void SaveUsers(List<User> users)
    {
        var serializerOptions = new JsonSerializerOptions() { WriteIndented = true };

        File.WriteAllText(UsersFileName, JsonSerializer.Serialize(users, serializerOptions));
    }
}