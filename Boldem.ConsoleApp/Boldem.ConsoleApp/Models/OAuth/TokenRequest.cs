using System.Text.Json.Serialization;

namespace Boldem.ConsoleApp.Models.OAuth;

public class TokenRequest
{
    [JsonPropertyName("client_id")]
    public string ClientId { get; set; } = string.Empty;

    [JsonPropertyName("client_secret")] 
    public string ClientSecret { get; set; } = string.Empty;
}