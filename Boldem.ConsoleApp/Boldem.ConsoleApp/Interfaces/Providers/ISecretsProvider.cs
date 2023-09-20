namespace Boldem.ConsoleApp.Interfaces.Providers;

public interface ISecretsProvider
{
    public (string Id, string Secret) Credentials();
}