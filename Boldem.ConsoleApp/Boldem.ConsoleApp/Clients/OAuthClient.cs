using System.Net.Http.Json;
using System.Text.Json;

using Boldem.ConsoleApp.Exceptions;
using Boldem.ConsoleApp.Interfaces.Clients;
using Boldem.ConsoleApp.Interfaces.Providers;
using Boldem.ConsoleApp.Models.OAuth;

namespace Boldem.ConsoleApp.Clients;

internal class OAuthClient : IOAuthClient
{
    private readonly HttpClient _client;
    
    private readonly ISecretsProvider _secrets;
    
    public OAuthClient(HttpClient client, ISecretsProvider secrets)
    {
        _client = client;
        _secrets = secrets;
    }
    
    private static Uri Token => new("/v1/oauth", UriKind.Relative);

    public async Task<TokenResponse> GetTokenAsync(CancellationToken token)
    {
        var (id, secret) = _secrets.Credentials();
        
        var result = await _client.PostAsJsonAsync(Token, new TokenRequest
        {
            ClientId = id,
            ClientSecret = secret,
        }, cancellationToken: token);
        result.EnsureSuccessStatusCode();

        var stream = await result.Content.ReadAsStreamAsync(token);
        var response = await JsonSerializer.DeserializeAsync<TokenResponse>(stream, cancellationToken: token);

        if (response is null) throw new BoldemBaseException("OAuth token response is empty");
        return response;
    }
}