using Manage_tasks_Biznes_Logic.Dtos.User;

namespace Manage_tasks_Biznes_Logic_Tests;

public class UserServiceTests : IDisposable
{
    private readonly UserService _sut;
    private readonly DbConnection _connection;
    private readonly DataBaseContext _context;

    public UserServiceTests()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        var contextOptions = new DbContextOptionsBuilder<DataBaseContext>()
             .UseSqlite(_connection)
             .Options;

        _context = new DataBaseContext(contextOptions);
        _context.Database.EnsureCreated();

        _sut = new UserService(_context);
    }

    #region GetUserById
    [Fact]
    public async Task GetUserById_ValidId_ReturnsUserWithReguestedId()
    {
        // Arrange
        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        await _context.UserEntities.AddAsync(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.GetUserById(user.Id);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(user.Id);
    }

    [Fact]
    public async Task GetUserById_InvalidId_ReturnsNull()
    {
        // Arrange

        // Act
        var result = await _sut.GetUserById(Guid.NewGuid());

        // Assert
        result.Should().BeNull();
    }
    #endregion

    [Fact]
    public async Task GetAllUsers_ReturnsAllUsersInDatabase()
    {
        // Arrange
        var users = new List<UserEntity>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Email = "email",
                PasswordSalt = new byte[1],
                PasswordHash = "hash",
                FirstName = "first",
                LastName = "Last"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Email = "email",
                PasswordSalt = new byte[1],
                PasswordHash = "hash",
                FirstName = "first",
                LastName = "Last"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Email = "email",
                PasswordSalt = new byte[1],
                PasswordHash = "hash",
                FirstName = "first",
                LastName = "Last"
            }
        };

        await _context.UserEntities.AddRangeAsync(users);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.GetAllUsers();

        // Assert
        result.Should().HaveCount(3);

        result.Select(u => u.Id).Should().Contain(users.Select(u => u.Id));
    }

    #region GetUserDetails
    [Fact]
    public async Task GetUserDetails_ValidId_ReturnsUserWithReguestedId()
    {
        // Arrange
        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        await _context.UserEntities.AddAsync(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.GetUserDetails(user.Id);

        // Assert
        result.UserId.Should().Be(user.Id);
    }

    [Fact]
    public async Task GetUserDetails_InvalidId_ThrowsException()
    {
        // Arrange

        // Act
        var action = async () => await _sut.GetUserDetails(Guid.NewGuid());

        // Assert
        await action.Should().ThrowAsync<InvalidOperationException>();
    }
    #endregion

    [Fact]
    public async Task EditUserDetails_EditsUserData()
    {
        // Arrange
        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        await _context.UserEntities.AddAsync(user);
        await _context.SaveChangesAsync();

        var dto = new UserDetailsDto
        {
            UserId = user.Id,
            FirstName = "NewFirst",
            LastName = "NewLast",
            Position = "NewPosition",
            DateOfBirth = new DateTime(1988, 1, 1)
        };

        // Act
        await _sut.EditUserDetails(dto);

        // Assert
        var userIdDb = await _context.UserEntities.FirstAsync();

        userIdDb.Id.Should().Be(dto.UserId);
        userIdDb.FirstName.Should().Be(dto.FirstName);
        userIdDb.LastName.Should().Be(dto.LastName);
        userIdDb.Position.Should().Be(dto.Position);
        userIdDb.DateOfBirth.Should().Be(dto.DateOfBirth);
    }

    public void Dispose()
    {
        _context.Dispose();
        _connection.Dispose();
    }
}