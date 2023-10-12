using System.Security.Claims;
using System.Security.Cryptography;
using Manage_tasks_Database.Context;
using Manage_tasks_Database.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Manage_tasks_Biznes_Logic.Dtos.Account;

namespace Manage_tasks_Biznes_Logic.Service
{
    public interface IAccountService
    {
        Task RegisterUser(RegisterDto dto);

        Task LoginUser(LoginDto dto);

        Task<EditEmailDto?> GetEditEmail(Guid userId);

        Task EditEmail(EditEmailDto dto);

        Task EditPassword(EditPasswordDto dto);

        Task DeleteAccount(DeleteAccountDto dto);
    }
    public class AccountService : IAccountService
    {
        private readonly DataBaseContext _dbContext;

        public AccountService(DataBaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task RegisterUser(RegisterDto dto)
        {
            dto.RegistrationFailed = false;

            if (dto.Password != dto.ConfirmPassword)
            {
                dto.PasswordsAreEqual = false;
                dto.RegistrationFailed = true;
            }
            else
            {
                dto.PasswordsAreEqual = true;
            }

            if (await _dbContext.UserEntities
                    .Select(u => u.Email)
                    .ContainsAsync(dto.Email))
            {
                dto.EmailAlreadyInUse = true;
                dto.RegistrationFailed = true;
            }
            else
            {
                dto.EmailAlreadyInUse = false;
            }

            if (dto.RegistrationFailed)
            {
                return;
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
        }

        public async Task LoginUser(LoginDto dto)
        {
            var user = await _dbContext.UserEntities
                .Where(u => u.Email == dto.Email)
                .Select(u => new { u.Id, u.PasswordHash, u.PasswordSalt })
                .FirstOrDefaultAsync();

            if (user is null || user.PasswordHash != GetHashedPassword(dto.Password, user.PasswordSalt))
            {
                dto.LoginWasSuccessful = false;

                return;
            }

            dto.LoginWasSuccessful = true;
            dto.ClaimsIdentity = GetClaimsIdentity(user.Id);
            dto.AuthProp = GetAuthProp(dto.RememberMe);
        }

        public async Task<EditEmailDto?> GetEditEmail(Guid userId)
        {
            var user = await _dbContext.UserEntities.FindAsync(userId);

            if (user is null)
            {
                return null;
            }

            var dto = new EditEmailDto
            {
                UserId = user.Id,
                CurrentEmail = user.Email
            };

            return dto;
        }

        public async Task EditEmail(EditEmailDto dto)
        {
            var user = await _dbContext.UserEntities.FindAsync(dto.UserId);

            if (user is null)
            {
                dto.EditEmailFailed = true;
                return;
            }

            if (dto.NewEmail == dto.CurrentEmail)
            {
                dto.EditEmailFailed = true;
                dto.NewEmailIsCurrentEmail = true;
                return;
            }

            dto.NewEmailIsCurrentEmail = false;

            if (await _dbContext.UserEntities
                    .Select(u => u.Email)
                    .ContainsAsync(dto.NewEmail))
            {
                dto.EditEmailFailed = true;
                dto.NewEmailAlreadyInUse = true;
                return;
            }

            dto.NewEmailAlreadyInUse = false;

            user.Email = dto.NewEmail;
            await _dbContext.SaveChangesAsync();

            dto.EditEmailFailed = false;
        }

        public async Task EditPassword(EditPasswordDto dto)
        {
            dto.EditPasswordFailed = false;

            var user = await _dbContext.UserEntities.FindAsync(dto.UserId);

            if (user is null)
            {
                dto.EditPasswordFailed = true;
                return;
            }

            if (user.PasswordHash != GetHashedPassword(dto.CurrentPassword, user.PasswordSalt))
            {

                dto.EditPasswordFailed = true;
                dto.WrongCurrentPassword = true;
            }
            else
            {
                dto.WrongCurrentPassword = false;
            }

            if (dto.NewPassword == dto.CurrentPassword)
            {
                dto.EditPasswordFailed = true;
                dto.NewPasswordIsOldPassword = true;
            }
            else
            {
                dto.NewPasswordIsOldPassword = false;
            }

            if (dto.NewPassword != dto.ConfirmNewPassword)
            {

                dto.EditPasswordFailed = true;
                dto.PasswordsAreEqual = false;
            }
            else
            {
                dto.PasswordsAreEqual = true;
            }

            if (dto.EditPasswordFailed)
            {
                return;
            }

            var salt = GeneratePasswordSalt();
            var passwordHash = GetHashedPassword(dto.NewPassword, salt);

            user.PasswordSalt = salt;
            user.PasswordHash = passwordHash;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAccount(DeleteAccountDto dto)
        {
            var user = await _dbContext.UserEntities.FindAsync(dto.UserId);

            if (user is null)
            {
                dto.DeleteAccountFailed = true;
                return;
            }

            if (user.PasswordHash != GetHashedPassword(dto.Password, user.PasswordSalt))
            {
                dto.DeleteAccountFailed = true;
                dto.WrongPassword = true;
                return;
            }

            _dbContext.Remove(user);
            await _dbContext.SaveChangesAsync();

            dto.DeleteAccountFailed = false;
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

        private static ClaimsIdentity GetClaimsIdentity(Guid userId)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Role, "User"),
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
}
