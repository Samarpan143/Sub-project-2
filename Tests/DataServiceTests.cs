using DataServiceLayer;
using Xunit;

namespace Tests;

public class DataServiceTests
{
    private readonly DataService _dataService;

    public DataServiceTests()
    {
        var connectionString = "Host=localhost;Database=portfolio_movie_test;Username=movieapp;Password=mysecretpassword";
        _dataService = new DataService(connectionString);
    }

    [Fact]
    public void GetTitles_ReturnsTitles()
    {
        var result = _dataService.GetTitles(0, 5);
        Assert.NotNull(result);
    }

    [Fact]
    public void GetPersons_ReturnsPersons()
    {
        var result = _dataService.GetPersons(0, 5);
        Assert.NotNull(result);
    }

    [Fact]
    public void CreateUser_ValidData_CreatesUser()
    {
        var username = $"testuser_{Guid.NewGuid()}";
        var user = _dataService.CreateUser(username, $"{username}@test.com", "testpass");

        Assert.NotNull(user);
        Assert.Equal(username, user.Username);
    }

    [Fact]
    public void ValidateUserCredentials_ValidCredentials_ReturnsTrue()
    {
        var username = $"testuser_{Guid.NewGuid()}";
        var password = "testpass";
        _dataService.CreateUser(username, $"{username}@test.com", password);

        var isValid = _dataService.ValidateUserCredentials(username, password);
        Assert.True(isValid);
    }

    [Fact]
    public void SimpleSearch_ReturnsResults()
    {
        var results = _dataService.SimpleSearch("test", 1);
        Assert.NotNull(results);
    }

    [Fact]
    public void GetGenres_ReturnsGenres()
    {
        var genres = _dataService.GetGenres();
        Assert.NotNull(genres);
    }

    [Fact]
    public void GetProfessions_ReturnsProfessions()
    {
        var professions = _dataService.GetProfessions();
        Assert.NotNull(professions);
    }
}