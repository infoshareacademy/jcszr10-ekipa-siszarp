using Manage_tasks_Biznes_Logic.Dtos.User;
using Manage_tasks_Biznes_Logic.Model;
using Manage_tasks_Database.Context;
using Manage_tasks_Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Manage_tasks_Biznes_Logic.Service;

public class UserService : IUserService
{
    private readonly DataBaseContext _dbContext;

    public UserService(DataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetUserById(Guid id)
    {
        var userEntity = await _dbContext.UserEntities.FindAsync(id);

        if (userEntity is null)
        {
            return null;
        }

        var user = ConvertUserEntity(userEntity);

        return user;
    }

    public async Task<List<User>> GetAllUsers()
    {
        var userEntities = await _dbContext.UserEntities.ToListAsync();

        var users = userEntities.Select(ConvertUserEntity).ToList();

        return users;
    }

    private static User ConvertUserEntity(UserEntity userEntity)
    {
        var user = new User
        {
            Id = userEntity.Id,
            FirstName = userEntity.FirstName,
            LastName = userEntity.LastName,
            Position = userEntity.Position ?? string.Empty
        };
        return user;
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
}