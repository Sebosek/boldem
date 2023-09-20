using Boldem.ConsoleApp.Models.OAuth;

namespace Boldem.ConsoleApp.Interfaces.Clients;

public interface IOAuthClient
{
    public Task<TokenResponse> GetTokenAsync(CancellationToken token);
}