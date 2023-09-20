using Boldem.ConsoleApp.Clients;
using Boldem.ConsoleApp.Providers;
using Boldem.ConsoleApp.Services;

var source = new CancellationTokenSource();
Console.CancelKeyPress += (_, eventArgs) =>
{
    source.Cancel();
    eventArgs.Cancel = true;
};

var token = source.Token;
var http = new HttpClient
{
    BaseAddress = new Uri("https://api.boldem.cz"),
};
var file = new XmlFileProvider();
var loader = new ContactsService(file);
var contacts = await loader.LoadContactsAsync(token);
var oauth = new OAuthClient(http, new SecretsProvider());
var auth = new AuthService(oauth);
var client = new ImportClient(http, auth);
var service = new ImportService(client);

try
{
    await service.ImportContactsAsync(contacts, token);
    Console.WriteLine("✅ Done!");
}
catch (Exception ex)
{
    Console.WriteLine("🔥 Import failed!");
    Console.WriteLine(ex.Message);
}