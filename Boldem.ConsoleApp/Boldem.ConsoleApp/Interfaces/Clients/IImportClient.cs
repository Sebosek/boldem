using Boldem.ConsoleApp.Models;

namespace Boldem.ConsoleApp.Interfaces.Clients;

public interface IImportClient
{
    public Task ImportContactsAsync(IEnumerable<Contact> contacts, CancellationToken token);
}