namespace Boldem.ConsoleApp.Models.Import;

public class ImportRequest
{
    public IEnumerable<ContactRequest> Contacts { get; set; } = Enumerable.Empty<ContactRequest>();
    
    public int ContactOverwriteMode { get; set; }
    
    public int PreImportAction { get; set; }

    public ImportRequest(IEnumerable<ContactRequest> contacts)
    {
        Contacts = contacts;
    }
}