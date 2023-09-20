namespace Boldem.ConsoleApp.Interfaces.Services;

public interface IAuthService
{
    public Task<string> GetTokenAsync(CancellationToken token);
}