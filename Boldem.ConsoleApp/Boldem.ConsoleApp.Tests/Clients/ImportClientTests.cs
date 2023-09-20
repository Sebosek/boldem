using Boldem.ConsoleApp.Clients;
using Boldem.ConsoleApp.Interfaces.Services;
using Boldem.ConsoleApp.Models;
using Boldem.ConsoleApp.Tests.Clients.Helpers;

using Moq;

namespace Boldem.ConsoleApp.Tests.Clients;

public class ImportClientTests
{
    private const string TOKEN = "...";
    
    private readonly Mock<IAuthService> _auth = new();

    private readonly Uri _host = new("http://test.local");

    public ImportClientTests()
    {
        _auth.Setup(s => s.GetTokenAsync(It.IsAny<CancellationToken>())).ReturnsAsync(TOKEN);
    }
    
    [Fact]
    public async Task ImportContactsAsync_ValidData_Success()
    {
        var http = CreateHttpClient(null!, (m, _) =>
        {
            Assert.Equal(new Uri(_host, "/v1/contacts-imports"), m.RequestUri);
            Assert.Equal(HttpMethod.Post, m.Method);
            Assert.Equal("Bearer", m.Headers.Authorization?.Scheme);
            Assert.Equal(TOKEN, m.Headers.Authorization?.Parameter);
        });
        
        var client = new ImportClient(http, _auth.Object);
        await client.ImportContactsAsync(Enumerable.Empty<Contact>(), CancellationToken.None);
        
        _auth.Verify(a => a.GetTokenAsync(It.IsAny<CancellationToken>()));
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