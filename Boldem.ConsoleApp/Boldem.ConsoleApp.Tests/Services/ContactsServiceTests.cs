using System.Xml.Linq;

using Boldem.ConsoleApp.Interfaces.Providers;
using Boldem.ConsoleApp.Interfaces.Services;
using Boldem.ConsoleApp.Services;

using Moq;

namespace Boldem.ConsoleApp.Tests.Services;

public class ContactsServiceTests
{
    private readonly IContactsService _service;
    
    private readonly Mock<IXmlDocumentProvider> _provider;

    public ContactsServiceTests()
    {
        _provider = new Mock<IXmlDocumentProvider>();
        _service = new ContactsService(_provider.Object);
    }
    
    [Fact]
    public async Task LoadContactsAsync_ValidData_Success()
    {
        var doc = new XDocument(new XElement("People", new XElement("Person",
            new XAttribute("name", "John"),
            new XAttribute("surname", "Doe"),
            new XAttribute("email", "john@doe.local")
        )));

        _provider.Setup(s => s.LoadAsync(It.IsAny<CancellationToken>())).ReturnsAsync(doc);
        var contacts = await _service.LoadContactsAsync(CancellationToken.None);
        
        Assert.All(contacts, c =>
        {
            Assert.NotEmpty(c.Name);
            Assert.NotEmpty(c.Surname);
            Assert.NotEmpty(c.Email);
        });
    }
    
    [Fact]
    public async Task LoadContactsAsync_OnlyRootElement_Success()
    {
        var doc = new XDocument(new XElement("People"));

        _provider.Setup(s => s.LoadAsync(It.IsAny<CancellationToken>())).ReturnsAsync(doc);
        var contacts = await _service.LoadContactsAsync(CancellationToken.None);
        
        Assert.Empty(contacts);
    }
    
    [Fact]
    public async Task LoadContactsAsync_MissingAttributes_Success()
    {
        var doc = new XDocument(new XElement("People", new XElement("Person")));

        _provider.Setup(s => s.LoadAsync(It.IsAny<CancellationToken>())).ReturnsAsync(doc);
        var contacts = await _service.LoadContactsAsync(CancellationToken.None);
        
        Assert.All(contacts, c =>
        {
            Assert.Empty(c.Name);
            Assert.Empty(c.Surname);
            Assert.Empty(c.Email);
        });
    }
}