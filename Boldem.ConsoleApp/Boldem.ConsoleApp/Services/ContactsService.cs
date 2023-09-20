using Boldem.ConsoleApp.Interfaces.Providers;
using Boldem.ConsoleApp.Interfaces.Services;
using Boldem.ConsoleApp.Models;

namespace Boldem.ConsoleApp.Services;

internal class ContactsService : IContactsService
{
    private readonly IXmlDocumentProvider _provider;

    public ContactsService(IXmlDocumentProvider provider)
    {
        _provider = provider;
    }

    public async Task<IReadOnlyCollection<Contact>> LoadContactsAsync(CancellationToken token)
    {
        var doc = await _provider.LoadAsync(token);
        var contacts = doc.Descendants("Person").Select(e =>
        {
            var attrs = e.Attributes().ToDictionary(k => k.Name, v => v);
            var name = string.Empty;
            var surname = string.Empty;
            var email = string.Empty;

            if (attrs.TryGetValue("name", out var nameAttr)) name = nameAttr.Value;
            if (attrs.TryGetValue("surname", out var surnameAttr)) surname = surnameAttr.Value;
            if (attrs.TryGetValue("email", out var emailAttr)) email = emailAttr.Value;

            return new Contact(name, surname, email);
        }).ToList();
        
        return contacts;
    }
}