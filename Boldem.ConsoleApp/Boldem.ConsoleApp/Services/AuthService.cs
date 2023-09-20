using Boldem.ConsoleApp.Interfaces.Clients;
using Boldem.ConsoleApp.Interfaces.Services;
using Boldem.ConsoleApp.Models.OAuth;

namespace Boldem.ConsoleApp.Services;

internal class AuthService : IAuthService
{
    private TokenResponse _response = null!;
    
    private readonly IOAuthClient _client;

    public AuthService(IOAuthClient client)
    {
        _client = client;
    }

    public async Task<string> GetTokenAsync(CancellationToken token)
    {
        _response ??= await _client.GetTokenAsync(token);

        return _response.AccessToken;
    }
}