using Manage_tasks_Biznes_Logic.Profiles;

namespace Manage_tasks_Biznes_Logic_Tests;

public class MapperTests
{
    [Fact]
    public void ValidateMapperProfiles()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddMaps(typeof(TeamProfile).Assembly));
        var mapper = config.CreateMapper();

        // Act
        var action = mapper.ConfigurationProvider.AssertConfigurationIsValid;

        // Assert
        action.Should().NotThrow();
    }
}
