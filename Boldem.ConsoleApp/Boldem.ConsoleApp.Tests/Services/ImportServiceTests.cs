using Boldem.ConsoleApp.Interfaces.Clients;
using Boldem.ConsoleApp.Interfaces.Services;
using Boldem.ConsoleApp.Models;
using Boldem.ConsoleApp.Services;

using Moq;

namespace Boldem.ConsoleApp.Tests.Services;

public class ImportServiceTests
{
    private readonly IImportService _service;

    private readonly Mock<IImportClient> _client;

    public ImportServiceTests()
    {
        _client = new Mock<IImportClient>();
        _service = new ImportService(_client.Object);
    }

    [Fact]
    public async Task ImportContactsAsync_EmptyData_Success()
    {
        await _service.ImportContactsAsync(Enumerable.Empty<Contact>(), CancellationToken.None);
        
        _client.Verify(a => a.ImportContactsAsync(It.IsAny<IEnumerable<Contact>>(), It.IsAny<CancellationToken>()), Times.Never);
    }
    
    [Fact]
    public async Task ImportContactsAsync_ValidData_Success()
    {
        await _service.ImportContactsAsync(new[]
        {
            new Contact("John", "Doe", "john@doe.local")
        }, CancellationToken.None);
        
        _client.Verify(a => a.ImportContactsAsync(It.IsAny<IEnumerable<Contact>>(), It.IsAny<CancellationToken>()));
    }
    
    [Fact]
    public Task ImportContactsAsync_NullCollection_ThrowException()
    {
        return Assert.ThrowsAsync<ArgumentNullException>(() => _service.ImportContactsAsync(null!, CancellationToken.None));
    }
}