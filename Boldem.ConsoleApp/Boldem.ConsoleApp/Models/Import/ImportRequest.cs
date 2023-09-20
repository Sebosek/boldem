namespace Boldem.ConsoleApp.Models.Import;

public class ImportRequest
{
    public IEnumerable<ContactRequest> Contacts { get; set; }

    public IEnumerable<int> MailingListIds { get; set; } = new[] { 1883 };

    public int ContactOverwriteMode { get; set; }
    
    public int PreImportAction { get; set; }
    
    public ImportRequest(IEnumerable<ContactRequest> contacts)
    {
        Contacts = contacts;
    }
}