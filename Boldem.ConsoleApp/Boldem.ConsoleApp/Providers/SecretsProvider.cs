using Boldem.ConsoleApp.Exceptions;
using Boldem.ConsoleApp.Interfaces.Providers;

namespace Boldem.ConsoleApp.Providers;

internal class SecretsProvider : ISecretsProvider
{
    public (string Id, string Secret) Credentials()
    {
        var id = Environment.GetEnvironmentVariable("BOLDEM_ID") ?? throw new BoldemNoCredentialsException();
        var secret = Environment.GetEnvironmentVariable("BOLDEM_SECRET") ?? throw new BoldemNoCredentialsException();
        
        return new(id, secret);
    }
}