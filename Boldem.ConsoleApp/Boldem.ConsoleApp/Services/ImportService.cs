using Boldem.ConsoleApp.Interfaces.Clients;
using Boldem.ConsoleApp.Interfaces.Services;
using Boldem.ConsoleApp.Models;

namespace Boldem.ConsoleApp.Services;

internal class ImportService : IImportService
{
    private readonly IImportClient _client;

    public ImportService(IImportClient client)
    {
        _client = client;
    }

    public async Task ImportContactsAsync(IEnumerable<Contact> contacts, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(contacts);
        
        var batches = contacts.Chunk(100).ToList();
        var size = batches.Count;
        var i = 1;
        
        foreach (var batch in batches)
        {
            await _client.ImportContactsAsync(batch, token);
            Console.WriteLine($"🚀 [{i++}/{size}] done");
        }
    }
}