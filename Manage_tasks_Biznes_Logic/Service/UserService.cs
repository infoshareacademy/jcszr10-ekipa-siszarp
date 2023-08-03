using Manage_tasks_Biznes_Logic.Model;
using System.Text.Json;

namespace Manage_tasks_Biznes_Logic.Service;

public class UserService
{
    private const string UsersFileName = "Users.json";

    public User? GetUserById(Guid id)
    {
        return GetAllUsers().Where(user => user.Id == id).FirstOrDefault();
    }

    public void UpdateUser(User user)
    {
        List<User> users = GetAllUsers();

        var userInDatabase = users.Where(u => u.Id == user.Id).FirstOrDefault();

        if (userInDatabase is not null)
        {
            users.Remove(userInDatabase);
        }

        users.Add(user);

        SaveUsers(users);
    }

    public void DeleteUser(User user)
    {
        List<User> users = GetAllUsers();

        var userInDatabase = users.Where(u => u.Id == user.Id).FirstOrDefault();

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

        if (users is null)
        {
            users = new ();
        }

        return users;
    }

    private void SaveUsers(List<User> users)
    {
        var serializerOptions = new JsonSerializerOptions() { WriteIndented = true };

        File.WriteAllText(UsersFileName, JsonSerializer.Serialize(users, serializerOptions));
    }
}