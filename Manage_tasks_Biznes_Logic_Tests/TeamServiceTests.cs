using Manage_tasks_Biznes_Logic.Dtos.Team;

namespace Manage_tasks_Biznes_Logic_Tests;

public class TeamServiceTests : IDisposable
{
    private readonly TeamService _sut;
    private readonly DbConnection _connection;
    private readonly DataBaseContext _context;
    private readonly IMapper _mapper;

    public TeamServiceTests()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        var contextOptions = new DbContextOptionsBuilder<DataBaseContext>()
             .UseSqlite(_connection)
             .Options;

        _context = new DataBaseContext(contextOptions);
        _context.Database.EnsureCreated();

        var config = new MapperConfiguration(cfg => cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));
        _mapper = config.CreateMapper();

        _sut = new TeamService(_context, _mapper);
    }

    [Fact]
    public async Task GetTeamById_ReturnsTeamWithRequestedId()
    {
        // Arrange
        var leader = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var team = new TeamEntity
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Leader = leader,
            Members = new List<UserEntity> { leader }
        };

        await _context.TeamEntities.AddAsync(team);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.GetTeamById(team.Id);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(team.Id);
    }

    [Fact]
    public async Task GetAllTeams_ReturnsAllTeamsInDatabase()
    {
        // Arrange
        var leader = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var teams = new List<TeamEntity>
        {
            new() {
                Id = Guid.NewGuid(),
                Name = "Name",
                Leader = leader,
                Members = new List<UserEntity> { leader }
            },
            new() {
                Id = Guid.NewGuid(),
                Name = "Name",
                Leader = leader,
                Members = new List<UserEntity> { leader }
            }
        };

        await _context.TeamEntities.AddRangeAsync(teams);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.GetAllTeams();

        // Assert
        result.Select(t => t.Id)
            .Should()
            .Contain(teams.Select(t => t.Id));
    }

    [Fact]
    public async Task GetTeamListForUser_ReturnsAllTeamsThatUserIsPartOf()
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

        var teamLeader = new TeamEntity
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Leader = user,
            Members = new List<UserEntity> { user }
        };

        var teamMember = new TeamEntity
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Leader = new UserEntity
            {
                Id = Guid.NewGuid(),
                Email = "email",
                PasswordSalt = new byte[1],
                PasswordHash = "hash",
                FirstName = "first",
                LastName = "Last"
            },
            Members = new List<UserEntity> { user }
        };

        await _context.TeamEntities.AddAsync(teamLeader);
        await _context.TeamEntities.AddAsync(teamMember);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.GetTeamListForUser(user.Id);

        // Assert
        result.TeamsLeader.Select(t => t.TeamId)
            .Should().Contain(teamLeader.Id);

        result.TeamsMember.Select(t => t.TeamId)
            .Should().Contain(teamMember.Id);
    }

    #region AddTeam
    [Fact]
    public async Task AddTeam_AddsNewTeamToDatabase()
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
        var dto = new TeamAddDto
        {
            Name = "New team",
            Description = "Description",
            LeaderId = user.Id
        };

        await _sut.AddTeam(dto);

        // Assert
        var teamsInDb = await _context.TeamEntities.ToListAsync();

        teamsInDb.Should()
            .ContainEquivalentOf(new { dto.Name, dto.Description });
    }

    [Fact]
    public async Task AddTeam_ThrowsWhenLeaderDoNotExist()
    {
        // Arrange

        // Act
        var newTeam = new TeamAddDto
        {
            Name = "New team",
            Description = "Description",
            LeaderId = Guid.NewGuid()
        };

        var action = async () => await _sut.AddTeam(newTeam);

        // Assert
        await action.Should().ThrowExactlyAsync<InvalidOperationException>()
            .WithMessage("Leader do not exist.");
    }
    #endregion

    #region DeleteTeam
    [Fact]
    public async Task DeleteTeam_DeletesTeamFormDatabase()
    {
        // Arrange
        var leader = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var team = new TeamEntity
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Leader = leader,
            Members = new List<UserEntity> { leader }
        };

        await _context.TeamEntities.AddAsync(team);
        await _context.SaveChangesAsync();

        // Act
        await _sut.DeleteTeam(team.Id, leader.Id);

        // Assert
        var teamsInDb = await _context.TeamEntities.ToListAsync();

        teamsInDb.Should().BeEmpty();
    }

    [Fact]
    public async Task DeleteTeam_ThrowsWhenTeamDoNotExists()
    {
        // Arrange

        // Act
        var action = async () => await _sut.DeleteTeam(Guid.NewGuid(), Guid.NewGuid());

        // Assert
        await action.Should().ThrowExactlyAsync<InvalidOperationException>()
            .WithMessage("Team do not exist.");
    }

    [Fact]
    public async Task DeleteTeam_ThrowsWhenEditorIsNotLeader()
    {
        // Arrange
        var leader = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var team = new TeamEntity
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Leader = leader,
            Members = new List<UserEntity> { leader }
        };

        await _context.TeamEntities.AddAsync(team);
        await _context.SaveChangesAsync();

        // Act
        var action = async () => await _sut.DeleteTeam(team.Id, Guid.NewGuid());

        // Assert
        await action.Should().ThrowExactlyAsync<UnauthorizedAccessException>()
            .WithMessage("Only the team leader can delete a team.");
    }
    #endregion

    #region GetTeamDetailsForUser
    [Fact]
    public async Task GetTeamDetailsForUser_ReturnsDetailsWithCanEditSetToTrue_WhenUserIsLeader()
    {
        // Arrange
        var leader = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var team = new TeamEntity
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Leader = leader,
            Members = new List<UserEntity> { leader }
        };

        await _context.TeamEntities.AddAsync(team);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.GetTeamDetailsForUser(team.Id, leader.Id);

        // Assert
        result.CanEditTeam.Should().BeTrue();
        result.TeamId.Should().Be(team.Id);
    }

    [Fact]
    public async Task GetTeamDetailsForUser_ReturnsDetailsWithCanEditSetToFalse_WhenUserIsNotLeader()
    {
        // Arrange
        var leader = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var team = new TeamEntity
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Leader = leader,
            Members = new List<UserEntity> { leader, user }
        };

        await _context.TeamEntities.AddAsync(team);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.GetTeamDetailsForUser(team.Id, user.Id);

        // Assert
        result.CanEditTeam.Should().BeFalse();
        result.TeamId.Should().Be(team.Id);
    }

    [Fact]
    public async Task GetTeamDetailsForUser_ThrowsWhenTeamDoNotExist()
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
        var action = async () => await _sut.GetTeamDetailsForUser(Guid.NewGuid(), user.Id);

        // Assert
        await action.Should().ThrowExactlyAsync<InvalidOperationException>()
            .WithMessage("Team do not exist, or the user is not a member of the team.");
    }

    [Fact]
    public async Task GetTeamDetailsForUser_ThrowsWhenUserIsNotMemberOfTeam()
    {
        // Arrange
        var leader = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var team = new TeamEntity
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Leader = leader,
            Members = new List<UserEntity> { leader }
        };

        await _context.TeamEntities.AddAsync(team);
        await _context.SaveChangesAsync();

        // Act
        var action = async () => await _sut.GetTeamDetailsForUser(team.Id, Guid.NewGuid());

        // Assert
        await action.Should().ThrowExactlyAsync<InvalidOperationException>()
            .WithMessage("Team do not exist, or the user is not a member of the team.");
    }
    #endregion

    #region GetTeamBasic
    [Fact]
    public async Task GetTeamBasic_ReturnsBasicInfoForTeam()
    {
        // Arrange
        var leader = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var team = new TeamEntity
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Leader = leader,
            Members = new List<UserEntity> { leader }
        };

        await _context.TeamEntities.AddAsync(team);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.GetTeamBasic(team.Id);

        // Assert
        result.TeamId.Should().Be(team.Id);
    }

    [Fact]
    public async Task GetTeamBasic_ThrowsWhenTeamDoNotExist()
    {
        // Arrange

        // Act
        var action = async () => await _sut.GetTeamBasic(Guid.NewGuid());

        // Assert
        await action.Should().ThrowExactlyAsync<InvalidOperationException>()
            .WithMessage("Team do not exist.");
    }
    #endregion

    #region EditTeam
    [Fact]
    public async Task EditTeam_EditsTeamWithNewData()
    {
        // Arrange
        var leader = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var team = new TeamEntity
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Leader = leader,
            Members = new List<UserEntity> { leader }
        };

        await _context.TeamEntities.AddAsync(team);
        await _context.SaveChangesAsync();

        // Act
        var dto = new TeamNameEditDto
        {
            TeamId = team.Id,
            EditorId = leader.Id,
            Name = "NewName",
            Description = "NewDescription"
        };

        await _sut.EditTeam(dto);

        // Assert
        var teamInDb = await _context.TeamEntities.FirstAsync();

        teamInDb.Should().NotBeNull();
        teamInDb.Name.Should().Be(dto.Name);
        teamInDb.Description.Should().Be(dto.Description);
    }

    [Fact]
    public async Task EditTeam_ThrowsWhenTeamDoNotExists()
    {
        // Arrange

        // Act
        var dto = new TeamNameEditDto
        {
            TeamId = Guid.NewGuid(),
            EditorId = Guid.NewGuid(),
            Name = "NewName",
            Description = "NewDescription"
        };

        var action = async () => await _sut.EditTeam(dto);

        // Assert
        await action.Should().ThrowExactlyAsync<InvalidOperationException>()
            .WithMessage("Team do not exist.");
    }

    [Fact]
    public async Task EditTeam_ThrowsWhenEditorIsNotLeader()
    {
        // Arrange
        var leader = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var team = new TeamEntity
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Leader = leader,
            Members = new List<UserEntity> { leader }
        };

        await _context.TeamEntities.AddAsync(team);
        await _context.SaveChangesAsync();

        // Act
        var dto = new TeamNameEditDto
        {
            TeamId = team.Id,
            EditorId = Guid.NewGuid(),
            Name = "NewName",
            Description = "NewDescription"
        };

        var action = async () => await _sut.EditTeam(dto);

        // Assert
        await action.Should().ThrowExactlyAsync<UnauthorizedAccessException>()
            .WithMessage("Only the team leader can edit team data.");
    }
    #endregion

    [Fact]
    public async Task GetAvailableTeamLeaders_ReturnsUsersThatCanBeLeaders()
    {
        // Arrange
        var leader = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var team = new TeamEntity
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Leader = leader,
            Members = new List<UserEntity> { leader, user }
        };

        await _context.TeamEntities.AddAsync(team);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.GetAvailableTeamLeaders(team.Id);

        // Assert
        result.Select(x => x.MemberId)
            .Should()
            .Contain(user.Id)
            .And.NotContain(leader.Id);
    }

    #region ChangeTeamLeader
    [Fact]
    public async Task ChangeTeamLeader_ChangesLeader()
    {
        // Arrange
        var leader = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var team = new TeamEntity
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Leader = leader,
            Members = new List<UserEntity> { leader, user }
        };

        await _context.TeamEntities.AddAsync(team);
        await _context.SaveChangesAsync();

        // Act
        var dto = new TeamChangeLeaderDto
        {
            TeamId = team.Id,
            EditorId = leader.Id,
            NewLeaderId = user.Id,
        };

        await _sut.ChangeTeamLeader(dto);

        // Assert
        var teamInDb = await _context.TeamEntities.FirstAsync();

        teamInDb.LeaderId.Should().Be(user.Id);
    }

    [Fact]
    public async Task ChangeTeamLeader_ThrowsWhenTeamDoNotExists()
    {
        // Arrange
        // Act
        var dto = new TeamChangeLeaderDto
        {
            TeamId = Guid.NewGuid(),
            EditorId = Guid.NewGuid(),
            NewLeaderId = Guid.NewGuid(),
        };

        var action = async () => await _sut.ChangeTeamLeader(dto);

        // Assert
        await action.Should().ThrowExactlyAsync<InvalidOperationException>()
             .WithMessage("Team do not exist.");
    }

    [Fact]
    public async Task ChangeTeamLeader_ThrowsWhenEditorIsNotLeader()
    {
        // Arrange
        var leader = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var team = new TeamEntity
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Leader = leader,
            Members = new List<UserEntity> { leader, user }
        };

        await _context.TeamEntities.AddAsync(team);
        await _context.SaveChangesAsync();

        // Act
        var dto = new TeamChangeLeaderDto
        {
            TeamId = team.Id,
            EditorId = Guid.NewGuid(),
            NewLeaderId = user.Id,
        };

        var action = async () => await _sut.ChangeTeamLeader(dto);

        // Assert
        await action.Should().ThrowExactlyAsync<UnauthorizedAccessException>()
             .WithMessage("Only the team leader can change the team leader.");
    }

    [Fact]
    public async Task ChangeTeamLeader_ThrowsWhenNewLeaderIsCurrentLeader()
    {
        // Arrange
        var leader = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var team = new TeamEntity
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Leader = leader,
            Members = new List<UserEntity> { leader, user }
        };

        await _context.TeamEntities.AddAsync(team);
        await _context.SaveChangesAsync();

        // Act
        var dto = new TeamChangeLeaderDto
        {
            TeamId = team.Id,
            EditorId = leader.Id,
            NewLeaderId = leader.Id,
        };

        var action = async () => await _sut.ChangeTeamLeader(dto);

        // Assert
        await action.Should().ThrowExactlyAsync<InvalidOperationException>()
             .WithMessage("New leader is the same as the current one.");
    }

    [Fact]
    public async Task ChangeTeamLeader_ThrowsWhenNewLeaderIsNotMember()
    {
        // Arrange
        var leader = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var team = new TeamEntity
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Leader = leader,
            Members = new List<UserEntity> { leader, user }
        };

        await _context.TeamEntities.AddAsync(team);
        await _context.SaveChangesAsync();

        // Act
        var dto = new TeamChangeLeaderDto
        {
            TeamId = team.Id,
            EditorId = leader.Id,
            NewLeaderId = Guid.NewGuid(),
        };

        var action = async () => await _sut.ChangeTeamLeader(dto);

        // Assert
        await action.Should().ThrowExactlyAsync<InvalidOperationException>()
             .WithMessage("New leader is not a member of the team.");
    }
    #endregion

    [Fact]
    public async Task GetAvailableTeamMembers_ReturnsUsersThatCanBeMembers()
    {
        // Arrange
        var leader = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var team = new TeamEntity
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Leader = leader,
            Members = new List<UserEntity> { leader }
        };

        await _context.TeamEntities.AddAsync(team);
        await _context.UserEntities.AddAsync(user);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.GetAvailableTeamMembers(team.Id);

        // Assert
        result.Select(x => x.MemberId)
            .Should().Contain(user.Id)
            .And.NotContain(leader.Id);
    }

    #region AddTeamMembers
    [Fact]
    public async Task AddTeamMembers_AddsNewMembersToTeam()
    {
        // Arrange
        var leader = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var newMember = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var team = new TeamEntity
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Leader = leader,
            Members = new List<UserEntity> { leader }
        };

        await _context.TeamEntities.AddAsync(team);
        await _context.UserEntities.AddAsync(newMember);
        await _context.SaveChangesAsync();

        // Act
        var dto = new TeamAddMembersDto
        {
            TeamId = team.Id,
            EditorId = leader.Id,
            NewMemberIds = new List<Guid> { newMember.Id }
        };

        await _sut.AddTeamMembers(dto);

        // Assert
        var teamInDb = await _context.TeamEntities
                .Include(t => t.Members)
                .FirstAsync();

        teamInDb.Members.Select(x => x.Id)
            .Should()
            .Contain(newMember.Id);
    }

    [Fact]
    public async Task AddTeamMembers_ThrowsWhenTeamDoNotExists()
    {
        // Arrange

        // Act
        var dto = new TeamAddMembersDto
        {
            TeamId = Guid.NewGuid(),
            EditorId = Guid.NewGuid(),
            NewMemberIds = new List<Guid>()
        };

        var action = async () => await _sut.AddTeamMembers(dto);

        // Assert
        await action.Should().ThrowExactlyAsync<InvalidOperationException>()
            .WithMessage("Team do not exist.");
    }

    [Fact]
    public async Task AddTeamMembers_ThrowsWhenEditorIsNotMember()
    {
        // Arrange
        var leader = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var newMember = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var team = new TeamEntity
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Leader = leader,
            Members = new List<UserEntity> { leader }
        };

        await _context.TeamEntities.AddAsync(team);
        await _context.UserEntities.AddAsync(newMember);
        await _context.SaveChangesAsync();

        // Act
        var dto = new TeamAddMembersDto
        {
            TeamId = team.Id,
            EditorId = Guid.NewGuid(),
            NewMemberIds = new List<Guid> { newMember.Id }
        };

        var action = async () => await _sut.AddTeamMembers(dto);

        // Assert
        await action.Should().ThrowExactlyAsync<UnauthorizedAccessException>()
            .WithMessage("Only the team leader can add members to the team.");
    }

    [Fact]
    public async Task AddTeamMembers_ThrowsWhenMemberIsAlreadyInTeam()
    {
        // Arrange
        var leader = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var team = new TeamEntity
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Leader = leader,
            Members = new List<UserEntity> { leader }
        };

        await _context.TeamEntities.AddAsync(team);
        await _context.SaveChangesAsync();

        // Act
        var dto = new TeamAddMembersDto
        {
            TeamId = team.Id,
            EditorId = leader.Id,
            NewMemberIds = new List<Guid> { leader.Id }
        };

        var action = async () => await _sut.AddTeamMembers(dto);

        // Assert
        await action.Should().ThrowExactlyAsync<InvalidOperationException>()
            .WithMessage("Some new members are already in the team.");
    }

    [Fact]
    public async Task AddTeamMembers_ThrowsWhenMemberDoNotExist()
    {
        // Arrange
        var leader = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var team = new TeamEntity
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Leader = leader,
            Members = new List<UserEntity> { leader }
        };

        await _context.TeamEntities.AddAsync(team);
        await _context.SaveChangesAsync();

        // Act
        var dto = new TeamAddMembersDto
        {
            TeamId = team.Id,
            EditorId = leader.Id,
            NewMemberIds = new List<Guid> { Guid.NewGuid() }
        };

        var action = async () => await _sut.AddTeamMembers(dto);

        // Assert
        await action.Should().ThrowExactlyAsync<InvalidOperationException>()
            .WithMessage("Some members do not exist.");
    }
    #endregion

    [Fact]
    public async Task GetAvailableTeamRemoveMembers_GetsAllMembersThatCanBeRemoved()
    {
        // Arrange
        var leader = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var team = new TeamEntity
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Leader = leader,
            Members = new List<UserEntity> { leader, user }
        };

        await _context.TeamEntities.AddAsync(team);
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.GetAvailableTeamRemoveMembers(team.Id);

        // Assert
        result.Select(x => x.MemberId)
            .Should()
            .Contain(user.Id)
            .And.NotContain(leader.Id);
    }

    #region RemoveTeamMembers
    [Fact]
    public async Task RemoveTeamMembers_RemovesMemebrsFromTeam()
    {
        // Arrange
        var leader = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var team = new TeamEntity
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Leader = leader,
            Members = new List<UserEntity> { leader, user }
        };

        await _context.TeamEntities.AddAsync(team);
        await _context.SaveChangesAsync();

        // Act
        var dto = new TeamRemoveMembersDto
        {
            TeamId = team.Id,
            EditorId = leader.Id,
            RemoveMemberIds = new List<Guid> { user.Id }
        };

        await _sut.RemoveTeamMembers(dto);

        // Assert
        var teamMembers = await _context.TeamEntities
            .SelectMany(t => t.Members)
            .Select(u => u.Id)
            .ToListAsync();

        teamMembers.Should()
            .Contain(leader.Id)
            .And.NotContain(user.Id);
    }

    [Fact]
    public async Task RemoveTeamMembers_ThrowsWhenTeamDoNotExists()
    {
        // Arrange

        // Act
        var dto = new TeamRemoveMembersDto
        {
            TeamId = Guid.NewGuid(),
            EditorId = Guid.NewGuid(),
            RemoveMemberIds = new List<Guid>()
        };

        var action = async () => await _sut.RemoveTeamMembers(dto);

        // Assert
        await action.Should().ThrowExactlyAsync<InvalidOperationException>()
            .WithMessage("Team do not exist.");
    }

    [Fact]
    public async Task RemoveTeamMembers_ThrowsWhenEditorIsNotLeaderAndDeletesOtherMember()
    {
        // Arrange
        var leader = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var firstMember = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var secondMember = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var team = new TeamEntity
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Leader = leader,
            Members = new List<UserEntity> { leader, firstMember, secondMember }
        };

        await _context.TeamEntities.AddAsync(team);
        await _context.SaveChangesAsync();

        // Act
        var dto = new TeamRemoveMembersDto
        {
            TeamId = team.Id,
            EditorId = firstMember.Id,
            RemoveMemberIds = new List<Guid> { secondMember.Id }
        };

        var action = async () => await _sut.RemoveTeamMembers(dto);

        // Assert
        await action.Should().ThrowExactlyAsync<UnauthorizedAccessException>()
            .WithMessage("Member can remove only themselves. Leader can remove other members.");
    }

    [Fact]
    public async Task RemoveTeamMembers_ThrowsWhenMemberToRemoveIsLeader()
    {
        // Arrange
        var leader = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var member = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var team = new TeamEntity
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Leader = leader,
            Members = new List<UserEntity> { leader, member }
        };

        await _context.TeamEntities.AddAsync(team);
        await _context.SaveChangesAsync();

        // Act
        var dto = new TeamRemoveMembersDto
        {
            TeamId = team.Id,
            EditorId = leader.Id,
            RemoveMemberIds = new List<Guid> { leader.Id }
        };

        var action = async () => await _sut.RemoveTeamMembers(dto);

        // Assert
        await action.Should().ThrowExactlyAsync<InvalidOperationException>()
            .WithMessage("One of the members to be removed is the leader.");
    }

    [Fact]
    public async Task RemoveTeamMembers_ThrowsWhenMemberIsNotPartOfTeam()
    {
        // Arrange
        var leader = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var member = new UserEntity
        {
            Id = Guid.NewGuid(),
            Email = "email",
            PasswordSalt = new byte[1],
            PasswordHash = "hash",
            FirstName = "first",
            LastName = "Last"
        };

        var team = new TeamEntity
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Leader = leader,
            Members = new List<UserEntity> { leader }
        };

        await _context.TeamEntities.AddAsync(team);
        await _context.UserEntities.AddAsync(member);
        await _context.SaveChangesAsync();

        // Act
        var dto = new TeamRemoveMembersDto
        {
            TeamId = team.Id,
            EditorId = leader.Id,
            RemoveMemberIds = new List<Guid> { member.Id }
        };

        var action = async () => await _sut.RemoveTeamMembers(dto);

        // Assert
        await action.Should().ThrowExactlyAsync<InvalidOperationException>()
            .WithMessage("Some members are not part of the team.");
    }
    #endregion

    public void Dispose()
    {
        _context.Dispose();
        _connection.Dispose();
    }
}
