using Boldem.ConsoleApp.Models;

namespace Boldem.ConsoleApp.Interfaces.Services;

public interface IImportService
{
    public Task ImportContactsAsync(IEnumerable<Contact> contacts, CancellationToken token);
}