using System.Xml.Linq;

namespace Boldem.ConsoleApp.Interfaces.Providers;

public interface IXmlDocumentProvider
{
    public Task<XDocument> LoadAsync(CancellationToken token);
}