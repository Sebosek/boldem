using System.Xml;
using System.Xml.Linq;

var source = new CancellationTokenSource();
Console.CancelKeyPress += (_, eventArgs) =>
{
    source.Cancel();
    eventArgs.Cancel = true;
};

var people = Enumerable.Range(0, 3000)
    .Select(_ =>
        CreatePersonElement(
            Faker.Name.First(),
            Faker.Name.Last(),
            Faker.Internet.Email())).ToArray();

var doc = new XDocument(new XElement("People", people));
await using var writer = XmlWriter.Create("people.xml", new XmlWriterSettings
{
    Indent = true,
    Async = true,
});

await doc.SaveAsync(writer, source.Token);
Console.WriteLine("✅ XML File generated");

static XElement CreatePersonElement(string name, string surname, string email) =>
    new("Person",
        new XAttribute("name", name),
        new XAttribute("surname", surname),
        new XAttribute("email", email)
    );