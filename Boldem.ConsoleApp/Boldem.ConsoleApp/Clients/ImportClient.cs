using System.Net.Http.Headers;
using System.Net.Http.Json;

using Boldem.ConsoleApp.Interfaces.Clients;
using Boldem.ConsoleApp.Interfaces.Services;
using Boldem.ConsoleApp.Models;
using Boldem.ConsoleApp.Models.Import;

namespace Boldem.ConsoleApp.Clients;

internal class ImportClient : IImportClient
{
    private readonly HttpClient _client;
    
    private readonly IAuthService _auth;

    public ImportClient(HttpClient client, IAuthService auth)
    {
        _client = client;
        _auth = auth;
    }
    
    private static Uri Import => new("/v1/contacts-imports", UriKind.Relative);

    public async Task ImportContactsAsync(IEnumerable<Contact> contacts, CancellationToken token)
    {
        var request = new ImportRequest(contacts.Select(s => new ContactRequest(s)));
        var message = new HttpRequestMessage
        {
            Headers =
            {
                Authorization = new AuthenticationHeaderValue("Bearer", await _auth.GetTokenAsync(token))
            },
            Content = JsonContent.Create(request),
            Method = HttpMethod.Post,
            RequestUri = Import,
        };

        var result = await _client.SendAsync(message, token);
        result.EnsureSuccessStatusCode();
    }
}