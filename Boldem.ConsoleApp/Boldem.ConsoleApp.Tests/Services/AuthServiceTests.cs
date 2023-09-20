using Boldem.ConsoleApp.Interfaces.Clients;
using Boldem.ConsoleApp.Models.OAuth;
using Boldem.ConsoleApp.Services;

using Moq;

namespace Boldem.ConsoleApp.Tests.Services;

public class AuthServiceTests
{
    private const string TOKEN = "...";
    
    private readonly Mock<IOAuthClient> _client;

    public AuthServiceTests()
    {
        _client = new Mock<IOAuthClient>();
        _client.Setup(a => a.GetTokenAsync(It.IsAny<CancellationToken>())).ReturnsAsync(new TokenResponse
        {
            AccessToken = TOKEN
        });
    }

    [Fact]
    public async Task GetTokenAsync_ValidData_Success()
    {
        var service = new AuthService(_client.Object);
        
        var token = await service.GetTokenAsync(CancellationToken.None);
        
        Assert.Equal(TOKEN, token);
    }
    
    [Fact]
    public async Task GetTokenAsync_LoadTokenOnce_Success()
    {
        var service = new AuthService(_client.Object);
        
        await service.GetTokenAsync(CancellationToken.None);
        await service.GetTokenAsync(CancellationToken.None);
        
        _client.Verify(a => a.GetTokenAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}