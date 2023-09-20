namespace Boldem.ConsoleApp.Models.Import;

public class ContactRequest
{
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public int Sex { get; set; }

    public ContactRequest()
    {
    }

    public ContactRequest(Contact contact)
    {
        FirstName = contact.Name;
        LastName = contact.Surname;
        Email = contact.Email;
        Sex = 0;
    }
}