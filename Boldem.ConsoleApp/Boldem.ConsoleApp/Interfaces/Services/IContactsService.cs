using Boldem.ConsoleApp.Models;

namespace Boldem.ConsoleApp.Interfaces.Services;

public interface IContactsService
{
    public Task<IReadOnlyCollection<Contact>> LoadContactsAsync(CancellationToken token);
}