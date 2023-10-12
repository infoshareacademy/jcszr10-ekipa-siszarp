using Manage_tasks_Biznes_Logic.Model;
using System.Text.Json;
using Manage_tasks_Biznes_Logic.Dtos.User;
using Manage_tasks_Database.Context;

namespace Manage_tasks_Biznes_Logic.Service;

public class UserService : IUserService
{
    private const string UsersFileName = "UserEntities.json";
    private readonly DataBaseContext _dbContext;

    public UserService(DataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

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

    public async Task<UserDetailsDto?> GetUserDetails(Guid userId)
    {
        var user = await _dbContext.UserEntities.FindAsync(userId);

        if (user is null)
        {
            return null;
        }

        var dto = new UserDetailsDto
        {
            UserId = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Position = user.Position,
            DateOfBirth = user.DateOfBirth
        };

        return dto;
    }

    public async Task EditUserDetails(UserDetailsDto dto)
    {
        var user = await _dbContext.UserEntities.FindAsync(dto.UserId);

        if (user is null)
        {
            dto.ChangesSaved = false;
            return;
        }

        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.Position = dto.Position;
        user.DateOfBirth = dto.DateOfBirth;

        await _dbContext.SaveChangesAsync();

        dto.ChangesSaved = true;
    }

    private void SaveUsers(List<User> users)
    {
        var serializerOptions = new JsonSerializerOptions() { WriteIndented = true };

        File.WriteAllText(UsersFileName, JsonSerializer.Serialize(users, serializerOptions));
    }
}