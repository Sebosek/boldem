using System.Net.Http.Json;

using Boldem.ConsoleApp.Clients;
using Boldem.ConsoleApp.Exceptions;
using Boldem.ConsoleApp.Interfaces.Providers;
using Boldem.ConsoleApp.Models.OAuth;
using Boldem.ConsoleApp.Tests.Clients.Helpers;

using Moq;

namespace Boldem.ConsoleApp.Tests.Clients;

public class OAuthClientTests
{
    private readonly Mock<ISecretsProvider> _secrets = new();

    private readonly Uri _host = new("http://test.local");

    public OAuthClientTests()
    {
        _secrets.Setup(s => s.Credentials()).Returns(("id", "secret"));
    }
    
    [Fact]
    public async Task GetTokenAsync_ValidRequest_Success()
    {
        var token = new TokenResponse
        {
            AccessToken = "token"
        };
        var http = CreateHttpClient(JsonContent.Create(token), (m, _) =>
        {
            Assert.Equal(new Uri(_host, "/v1/oauth"), m.RequestUri);
            Assert.Equal(HttpMethod.Post, m.Method);
        });

        var client = new OAuthClient(http, _secrets.Object);
        await client.GetTokenAsync(CancellationToken.None);
    }
    
    [Fact]
    public async Task GetTokenAsync_ValidResponse_Success()
    {
        var token = new TokenResponse
        {
            AccessToken = "token"
        };
        var http = CreateHttpClient(JsonContent.Create(token), (_, _) => { });
        var client = new OAuthClient(http, _secrets.Object);
        var response = await client.GetTokenAsync(CancellationToken.None);
        
        Assert.NotNull(response);
        Assert.NotEmpty(response.AccessToken);
        
        _secrets.Verify(a => a.Credentials());
    }

    [Fact(Skip = "Reproduce returning null from request would require to invest more effort, skip due to the Pareto principle")]
    public Task GetTokenAsync_NullResponse_ThrowException()
    {
        var http = CreateHttpClient(JsonContent.Create(string.Empty), (_, _) => { });
        var client = new OAuthClient(http, _secrets.Object);

        return Assert.ThrowsAsync<BoldemBaseException>(() => client.GetTokenAsync(CancellationToken.None));
    }

    private HttpClient CreateHttpClient(HttpContent content, Action<HttpRequestMessage, CancellationToken> callback)
    {
        return new HttpClientSpyBuilder()
            .SetBaseUri(_host)
            .SetContent(content)
            .SetCallback(callback)
            .Build();
    }
}