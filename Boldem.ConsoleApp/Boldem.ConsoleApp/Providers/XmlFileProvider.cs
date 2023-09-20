using System.Xml;
using System.Xml.Linq;
using Boldem.ConsoleApp.Interfaces.Providers;

namespace Boldem.ConsoleApp.Providers;

public class XmlFileProvider : IXmlDocumentProvider
{
    public async Task<XDocument> LoadAsync(CancellationToken token)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "people.xml");
        using var reader = XmlReader.Create(path, new XmlReaderSettings
        {
            Async = true
        });
        
        return await XDocument.LoadAsync(reader, LoadOptions.None, token);
    }
}