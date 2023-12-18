using Manage_tasks_Biznes_Logic.Dtos.Account;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Manage_tasks_Biznes_Logic_Tests;

public class AccountServiceTests
{
    private readonly AccountService _sut;
    private readonly DbConnection _connection;
    private readonly DataBaseContext _context;

    public AccountServiceTests()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        var contextOptions = new DbContextOptionsBuilder<DataBaseContext>()
             .UseSqlite(_connection)
             .Options;

        _context = new DataBaseContext(contextOptions);
        _context.Database.EnsureCreated();

        _sut = new AccountService(_context);
    }

    #region RegisterAccount
    [Fact]
    public async Task RegisterAccount_ValidDto_RegistersAccount()
    {
        // Arrange
        var fixture = new Fixture();

        var dto = fixture.Create<RegisterDto>();
        dto.ConfirmPassword = dto.Password;

        // Act
        var result = await _sut.RegisterAccount(dto);

        // Assert
        result.RegistrationFailed.Should().BeFalse();
        result.PasswordsAreEqual.Should().BeTrue();
        result.EmailAlreadyInUse.Should().BeFalse();

        var userInDb = await _context.UserEntities.FirstAsync();

        userInDb.FirstName.Should().Be(dto.FirstName);
        userInDb.LastName.Should().Be(dto.LastName);
        userInDb.Email.Should().Be(dto.Email);

        var hash = Convert.ToBase64String(
            KeyDerivation.Pbkdf2(
                dto.Password,
                userInDb.PasswordSalt,
                KeyDerivationPrf.HMACSHA256,
                100_000,
                256 / 8));

        userInDb.PasswordHash.Should().Be(hash);
    }

    [Fact]
    public async Task RegisterAccount_EmailAlreadyInUse_FailsToCreateAccount()
    {
        // Arrange
        var fixture = new Fixture();

        var dto = fixture.Create<RegisterDto>();
        dto.ConfirmPassword = dto.Password;

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = dto.Email,
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        await _context.UserEntities.AddAsync(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.RegisterAccount(dto);

        // Assert
        result.RegistrationFailed.Should().BeTrue();
        result.PasswordsAreEqual.Should().BeTrue();
        result.EmailAlreadyInUse.Should().BeTrue();

        var userInDb = await _context.UserEntities.FirstOrDefaultAsync(u => u.Id != user.Id);
        userInDb.Should().BeNull();
    }

    [Fact]
    public async Task RegisterAccount_PasswordsNotEqual_FailsToCreateAccount()
    {
        // Arrange
        var fixture = new Fixture();

        var dto = fixture.Create<RegisterDto>();

        // Act
        var result = await _sut.RegisterAccount(dto);

        // Assert
        result.RegistrationFailed.Should().BeTrue();
        result.PasswordsAreEqual.Should().BeFalse();
        result.EmailAlreadyInUse.Should().BeFalse();

        var userInDb = await _context.UserEntities.FirstOrDefaultAsync();
        userInDb.Should().BeNull();
    }

    [Fact]
    public async Task RegisterAccount_EmailAlreadyInUseAndPasswordsAreNotEqual_FailsToCreateAccount()
    {
        // Arrange
        var fixture = new Fixture();

        var dto = fixture.Create<RegisterDto>();

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = dto.Email,
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        await _context.UserEntities.AddAsync(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.RegisterAccount(dto);

        // Assert
        result.RegistrationFailed.Should().BeTrue();
        result.PasswordsAreEqual.Should().BeFalse();
        result.EmailAlreadyInUse.Should().BeTrue();

        var userInDb = await _context.UserEntities.FirstOrDefaultAsync(u => u.Id != user.Id);
        userInDb.Should().BeNull();
    }
    #endregion

    #region LoginAccount
    [Fact]
    public async Task LoginAccount_ValidDto_ReturnsDtoWithDataForAuthentication()
    {
        // Arrange
        var password = "password";
        var email = "email";
        var salt = RandomNumberGenerator.GetBytes(128 / 8);

        var hash = Convert.ToBase64String(
            KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA256,
            100_000,
            256 / 8));

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = email,
            PasswordSalt = salt,
            PasswordHash = hash,
            FirstName = "first",
            LastName = "Last"
        };

        var dto = new LoginDto
        {
            Email = email,
            Password = password,
        };

        await _context.UserEntities.AddAsync(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.LoginAccount(dto);

        // Assert
        result.LoginWasSuccessful.Should().BeTrue();

        result.UserId.Should().NotBeNull();
        result.AuthProp.Should().NotBeNull();
        result.ClaimsIdentity.Should().NotBeNull();

        result.UserId.Should().Be(user.Id);
    }

    [Fact]
    public async Task LoginAccount_InvalidEmail_FailsToAuthenticate()
    {
        // Arrange
        var password = "password";
        var email = "email";

        var dto = new LoginDto
        {
            Email = email,
            Password = password,
        };

        // Act
        var result = await _sut.LoginAccount(dto);

        // Assert
        result.LoginWasSuccessful.Should().BeFalse();

        result.UserId.Should().BeNull();
        result.AuthProp.Should().BeNull();
        result.ClaimsIdentity.Should().BeNull();
    }

    [Fact]
    public async Task LoginAccount_InvalidPassword_FailsToAuthenticate()
    {
        // Arrange
        var password = "password";
        var email = "email";
        var salt = RandomNumberGenerator.GetBytes(128 / 8);

        var hash = Convert.ToBase64String(
            KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA256,
            100_000,
            256 / 8));

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = email,
            PasswordSalt = salt,
            PasswordHash = hash,
            FirstName = "first",
            LastName = "Last"
        };

        var dto = new LoginDto
        {
            Email = email,
            Password = "PASSWORD",
        };

        await _context.UserEntities.AddAsync(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.LoginAccount(dto);

        // Assert
        result.LoginWasSuccessful.Should().BeFalse();

        result.UserId.Should().BeNull();
        result.AuthProp.Should().BeNull();
        result.ClaimsIdentity.Should().BeNull();
    }
    #endregion

    #region GetAccountEmail
    [Fact]
    public async Task GetAccountEmail_ValidUserId_ReturnsEmailOfUser()
    {
        // Arrange
        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "Email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        await _context.UserEntities.AddAsync(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.GetAccountEmail(user.Id);

        // Assert
        result.Should().Be(user.Email);
    }

    [Fact]
    public async Task GetAccountEmail_InvalidUserId_ThrowsException()
    {
        // Arrange

        // Act
        var action = async () => await _sut.GetAccountEmail(Guid.NewGuid());

        // Assert
        await action.Should().ThrowExactlyAsync<InvalidOperationException>();
    }
    #endregion

    #region EditAccountEmail
    [Fact]
    public async Task EditAccountEmail_ValidDto_ChangesEmail()
    {
        // Arrange
        var id = Guid.NewGuid();

        var user = new UserEntity
        {
            Id = id,
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        await _context.UserEntities.AddAsync(user);
        await _context.SaveChangesAsync();

        var dto = new EditEmailDto
        {
            UserId = id,
            NewEmail = "EMAIL"
        };

        // Act
        var result = await _sut.EditAccountEmail(dto);

        // Assert
        result.EditEmailFailed.Should().BeFalse();
        result.NewEmailAlreadyInUse.Should().BeFalse();
        result.NewEmailIsCurrentEmail.Should().BeFalse();

        var userInDb = await _context.UserEntities.FirstAsync();
        userInDb.Email.Should().Be(dto.NewEmail);
    }

    [Fact]
    public async Task EditAccountEmail_InvalidUserId_ThrowsException()
    {
        // Arrange
        var dto = new EditEmailDto
        {
            UserId = Guid.NewGuid(),
            NewEmail = "EMAIL"
        };

        // Act
        var action = async () => await _sut.EditAccountEmail(dto);

        // Assert
        await action.Should().ThrowExactlyAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task EditAccountEmail_NewEmailIsCurrentEmail_FailsToChangeEmail()
    {
        // Arrange
        var id = Guid.NewGuid();
        var email = "email";

        var user = new UserEntity
        {
            Id = id,
            Email = email,
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        await _context.UserEntities.AddAsync(user);
        await _context.SaveChangesAsync();

        var dto = new EditEmailDto
        {
            UserId = id,
            NewEmail = email
        };

        // Act
        var result = await _sut.EditAccountEmail(dto);

        // Assert
        result.EditEmailFailed.Should().BeTrue();
        result.NewEmailAlreadyInUse.Should().BeFalse();
        result.NewEmailIsCurrentEmail.Should().BeTrue();
    }

    [Fact]
    public async Task EditAccountEmail_NewEmailIsInUse_FailsToChangeEmail()
    {
        // Arrange
        var id = Guid.NewGuid();
        var email = "EMAIL";

        var user = new UserEntity
        {
            Id = id,
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var otherUser = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = email,
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        await _context.UserEntities.AddAsync(user);
        await _context.UserEntities.AddAsync(otherUser);
        await _context.SaveChangesAsync();

        var dto = new EditEmailDto
        {
            UserId = id,
            NewEmail = email
        };

        // Act
        var result = await _sut.EditAccountEmail(dto);

        // Assert
        result.EditEmailFailed.Should().BeTrue();
        result.NewEmailAlreadyInUse.Should().BeTrue();
        result.NewEmailIsCurrentEmail.Should().BeFalse();

        var userInDb = await _context.UserEntities.FirstAsync(u => u.Id == user.Id);
        userInDb.Email.Should().Be(user.Email);
    }
    #endregion

    #region EditAccountPassword
    [Fact]
    public async Task EditAccountPassword_ValidDto_ChangesPassword()
    {
        // Arrange
        var password = "password";
        var newPassword = "newPassword";
        var email = "email";
        var salt = RandomNumberGenerator.GetBytes(128 / 8);

        var hash = Convert.ToBase64String(
            KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA256,
            100_000,
            256 / 8));

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = email,
            PasswordSalt = salt,
            PasswordHash = hash,
            FirstName = "first",
            LastName = "Last"
        };

        var dto = new EditPasswordDto
        {
            UserId = user.Id,
            CurrentPassword = password,
            NewPassword = newPassword,
            ConfirmNewPassword = newPassword
        };

        await _context.UserEntities.AddAsync(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.EditAccountPassword(dto);

        // Assert
        result.EditPasswordFailed.Should().BeFalse();
        result.WrongCurrentPassword.Should().BeFalse();
        result.NewPasswordIsOldPassword.Should().BeFalse();
        result.PasswordsAreEqual.Should().BeTrue();

        var userInDb = await _context.UserEntities.FirstAsync();

        var newHash = Convert.ToBase64String(
            KeyDerivation.Pbkdf2(
                newPassword,
                userInDb!.PasswordSalt,
                KeyDerivationPrf.HMACSHA256,
                100_000,
                256 / 8));

        userInDb.PasswordHash.Should().Be(newHash);
    }

    [Fact]
    public async Task EditAccountPassword_WrongCurrentPassword_FailsToChangesPassword()
    {
        // Arrange
        var password = "password";
        var newPassword = "newPassword";
        var email = "email";
        var salt = RandomNumberGenerator.GetBytes(128 / 8);

        var hash = Convert.ToBase64String(
            KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA256,
            100_000,
            256 / 8));

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = email,
            PasswordSalt = salt,
            PasswordHash = hash,
            FirstName = "first",
            LastName = "Last"
        };

        var dto = new EditPasswordDto
        {
            UserId = user.Id,
            CurrentPassword = "PASSWORD",
            NewPassword = newPassword,
            ConfirmNewPassword = newPassword
        };

        await _context.UserEntities.AddAsync(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.EditAccountPassword(dto);

        // Assert
        result.EditPasswordFailed.Should().BeTrue();
        result.WrongCurrentPassword.Should().BeTrue();
        result.NewPasswordIsOldPassword.Should().BeFalse();
        result.PasswordsAreEqual.Should().BeTrue();

        var userInDb = await _context.UserEntities.FirstAsync();

        userInDb.PasswordHash.Should().Be(hash);
    }

    [Fact]
    public async Task EditAccountPassword_NewPasswordIsOldPassword_FailsToChangesPassword()
    {
        // Arrange
        var password = "password";
        var email = "email";
        var salt = RandomNumberGenerator.GetBytes(128 / 8);

        var hash = Convert.ToBase64String(
            KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA256,
            100_000,
            256 / 8));

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = email,
            PasswordSalt = salt,
            PasswordHash = hash,
            FirstName = "first",
            LastName = "Last"
        };

        var dto = new EditPasswordDto
        {
            UserId = user.Id,
            CurrentPassword = password,
            NewPassword = password,
            ConfirmNewPassword = password
        };

        await _context.UserEntities.AddAsync(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.EditAccountPassword(dto);

        // Assert
        result.EditPasswordFailed.Should().BeTrue();
        result.WrongCurrentPassword.Should().BeFalse();
        result.NewPasswordIsOldPassword.Should().BeTrue();
        result.PasswordsAreEqual.Should().BeTrue();

        var userInDb = await _context.UserEntities.FirstAsync();

        userInDb.PasswordHash.Should().Be(hash);
    }

    [Fact]
    public async Task EditAccountPassword_PasswordsArentEqual_FailsToChangesPassword()
    {
        // Arrange
        var password = "password";
        var email = "email";
        var salt = RandomNumberGenerator.GetBytes(128 / 8);

        var hash = Convert.ToBase64String(
            KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA256,
            100_000,
            256 / 8));

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = email,
            PasswordSalt = salt,
            PasswordHash = hash,
            FirstName = "first",
            LastName = "Last"
        };

        var dto = new EditPasswordDto
        {
            UserId = user.Id,
            CurrentPassword = password,
            NewPassword = "password123",
            ConfirmNewPassword = "pasword456"
        };

        await _context.UserEntities.AddAsync(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.EditAccountPassword(dto);

        // Assert
        result.EditPasswordFailed.Should().BeTrue();
        result.WrongCurrentPassword.Should().BeFalse();
        result.NewPasswordIsOldPassword.Should().BeFalse();
        result.PasswordsAreEqual.Should().BeFalse();

        var userInDb = await _context.UserEntities.FirstAsync();

        userInDb.PasswordHash.Should().Be(hash);
    }

    [Fact]
    public async Task EditAccountPassword_InvalidUserId_ThrowsException()
    {
        // Arrange

        var dto = new EditPasswordDto
        {
            UserId = Guid.NewGuid(),
            CurrentPassword = "password",
            NewPassword = "password123",
            ConfirmNewPassword = "password123"
        };

        // Act
        var action = async () => await _sut.EditAccountPassword(dto);

        // Assert
        await action.Should().ThrowExactlyAsync<InvalidOperationException>();
    }
    #endregion

    #region DeleteAccount
    [Fact]
    public async Task DeleteAccount_ValidDto_DeletesAccountAndReturnsTrue()
    {
        // Arrange
        var id = Guid.NewGuid();
        var password = "password";
        var email = "email";
        var salt = RandomNumberGenerator.GetBytes(128 / 8);

        var hash = Convert.ToBase64String(
            KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA256,
            100_000,
            256 / 8));

        var user = new UserEntity
        {
            Id = id,
            Email = email,
            PasswordSalt = salt,
            PasswordHash = hash,
            FirstName = "first",
            LastName = "Last"
        };

        var dto = new DeleteAccountDto
        {
            UserId = id,
            Password = password,
        };

        await _context.UserEntities.AddAsync(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.DeleteAccount(dto);

        // Assert
        result.Should().BeTrue();

        var userInDb = await _context.UserEntities.FirstOrDefaultAsync();
        userInDb.Should().BeNull();
    }

    [Fact]
    public async Task DeleteAccount_InvalidPassword_ReturnsTrueFalse()
    {
        // Arrange
        var id = Guid.NewGuid();
        var email = "email";
        var salt = RandomNumberGenerator.GetBytes(128 / 8);

        var hash = Convert.ToBase64String(
            KeyDerivation.Pbkdf2(
            "password",
            salt,
            KeyDerivationPrf.HMACSHA256,
            100_000,
            256 / 8));

        var user = new UserEntity
        {
            Id = id,
            Email = email,
            PasswordSalt = salt,
            PasswordHash = hash,
            FirstName = "first",
            LastName = "Last"
        };

        var dto = new DeleteAccountDto
        {
            UserId = id,
            Password = "PASSWORD",
        };

        await _context.UserEntities.AddAsync(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.DeleteAccount(dto);

        // Assert
        result.Should().BeFalse();

        var userInDb = await _context.UserEntities.FirstOrDefaultAsync();
        userInDb.Should().NotBeNull();
    }

    [Fact]
    public async Task DeleteAccount_InvalidUserId_ThrowsException()
    {
        // Arrange

        var dto = new DeleteAccountDto
        {
            UserId = Guid.NewGuid(),
            Password = string.Empty,
        };

        // Act
        var action = async () => await _sut.DeleteAccount(dto);

        // Assert
        await action.Should().ThrowExactlyAsync<InvalidOperationException>();
    } 
    #endregion
}
