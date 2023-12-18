using System.Security.Claims;
using System.Security.Cryptography;
using Manage_tasks_Database.Context;
using Manage_tasks_Database.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Manage_tasks_Biznes_Logic.Dtos.Account;

namespace Manage_tasks_Biznes_Logic.Service;

public interface IAccountService
{
    Task<RegisterResultDto> RegisterAccount(RegisterDto dto);

    Task<LoginResultDto> LoginAccount(LoginDto dto);

    Task<string> GetAccountEmail(Guid userId);

    Task<EditEmailResultDto> EditAccountEmail(EditEmailDto dto);

    Task<EditPasswordResultDto> EditAccountPassword(EditPasswordDto dto);

    Task<bool> DeleteAccount(DeleteAccountDto dto);
}

public class AccountService : IAccountService
{
    private readonly DataBaseContext _dbContext;

    public AccountService(DataBaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<RegisterResultDto> RegisterAccount(RegisterDto dto)
    {
        var resultDto = new RegisterResultDto();

        if (dto.Password != dto.ConfirmPassword)
        {
            resultDto.PasswordsAreEqual = false;
            resultDto.RegistrationFailed = true;
        }
        else
        {
            resultDto.PasswordsAreEqual = true;
        }

        if (await _dbContext.UserEntities
                .Select(u => u.Email)
                .ContainsAsync(dto.Email))
        {
            resultDto.EmailAlreadyInUse = true;
            resultDto.RegistrationFailed = true;
        }
        else
        {
            resultDto.EmailAlreadyInUse = false;
        }

        if (resultDto.RegistrationFailed)
        {
            return resultDto;
        }

        var salt = GeneratePasswordSalt();
        var passwordHash = GetHashedPassword(dto.Password, salt);

        var newUser = new UserEntity
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            PasswordSalt = salt,
            PasswordHash = passwordHash
        };

        await _dbContext.UserEntities.AddAsync(newUser);
        await _dbContext.SaveChangesAsync();

        return resultDto;
    }

    public async Task<LoginResultDto> LoginAccount(LoginDto dto)
    {
        var user = await _dbContext.UserEntities
            .Where(u => u.Email == dto.Email)
            .Select(u => new { u.Id, u.PasswordHash, u.PasswordSalt, name = $"{u.FirstName} {u.LastName}" })
            .FirstOrDefaultAsync();

        var resultDto = new LoginResultDto();

        if (user is null || user.PasswordHash != GetHashedPassword(dto.Password, user.PasswordSalt))
        {
            resultDto.LoginWasSuccessful = false;

            return resultDto;
        }

        resultDto.LoginWasSuccessful = true;
        resultDto.UserId = user.Id;
        resultDto.ClaimsIdentity = GetClaimsIdentity(user.Id, user.name);
        resultDto.AuthProp = GetAuthProp(dto.RememberMe);

        return resultDto;
    }

    public async Task<string> GetAccountEmail(Guid userId)
    {
        var email = await _dbContext.UserEntities
            .Where(u => u.Id == userId)
            .Select(u => u.Email)
            .FirstAsync();

        return email;
    }

    public async Task<EditEmailResultDto> EditAccountEmail(EditEmailDto dto)
    {
        var user = await _dbContext.UserEntities.FirstAsync(u => u.Id == dto.UserId);

        var resultDto = new EditEmailResultDto();

        if (dto.NewEmail == user.Email)
        {
            resultDto.EditEmailFailed = true;
            resultDto.NewEmailIsCurrentEmail = true;
            return resultDto;
        }

        if (await _dbContext.UserEntities
                .Select(u => u.Email)
                .ContainsAsync(dto.NewEmail))
        {
            resultDto.EditEmailFailed = true;
            resultDto.NewEmailAlreadyInUse = true;
            return resultDto;
        }

        user.Email = dto.NewEmail;
        await _dbContext.SaveChangesAsync();

        return resultDto;
    }

    public async Task<EditPasswordResultDto> EditAccountPassword(EditPasswordDto dto)
    {
        var user = await _dbContext.UserEntities.FirstAsync(u => u.Id == dto.UserId);

        var resultDto = new EditPasswordResultDto();

        if (user.PasswordHash != GetHashedPassword(dto.CurrentPassword, user.PasswordSalt))
        {

            resultDto.EditPasswordFailed = true;
            resultDto.WrongCurrentPassword = true;
        }

        if (dto.NewPassword == dto.CurrentPassword)
        {
            resultDto.EditPasswordFailed = true;
            resultDto.NewPasswordIsOldPassword = true;
        }

        if (dto.NewPassword != dto.ConfirmNewPassword)
        {

            resultDto.EditPasswordFailed = true;
            resultDto.PasswordsAreEqual = false;
        }
        else
        {
            resultDto.PasswordsAreEqual = true;
        }

        if (resultDto.EditPasswordFailed)
        {
            return resultDto;
        }

        var salt = GeneratePasswordSalt();
        var passwordHash = GetHashedPassword(dto.NewPassword, salt);

        user.PasswordSalt = salt;
        user.PasswordHash = passwordHash;
        await _dbContext.SaveChangesAsync();

        return resultDto;
    }

    public async Task<bool> DeleteAccount(DeleteAccountDto dto)
    {
        var user = await _dbContext.UserEntities.FirstAsync(u => u.Id == dto.UserId);

        if (user.PasswordHash != GetHashedPassword(dto.Password, user.PasswordSalt))
        {
            return false;
        }

        _dbContext.Remove(user);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    private static byte[] GeneratePasswordSalt()
    {
        return RandomNumberGenerator.GetBytes(128 / 8);
    }

    private static string GetHashedPassword(string password, byte[] salt)
    {
        var hash = KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA256,
            100_000,
            256 / 8);

        return Convert.ToBase64String(hash);
    }

    private static ClaimsIdentity GetClaimsIdentity(Guid userId, string name)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Role, "User"),
            new(ClaimTypes.Name, name),
            new("UserId", userId.ToString())
        };

        return new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    }

    private static AuthenticationProperties GetAuthProp(bool rememberMe)
    {
        var authProperties = new AuthenticationProperties
        {
            AllowRefresh = true,
            IsPersistent = rememberMe
        };

        return authProperties;
    }
}