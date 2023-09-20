using System.Text.Json.Serialization;

namespace Boldem.ConsoleApp.Models.OAuth;

public class TokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;

    [JsonPropertyName("token_type")] 
    public string TokenType { get; set; } = string.Empty;
    
    [JsonPropertyName("refresh_token")] 
    public string RefreshToken { get; set; } = string.Empty;
    
    [JsonPropertyName("valid_to")] 
    public DateTime ValidTo { get; set; }
    
    [JsonPropertyName("expires_in")] 
    public int ExpiresIn { get; set; }
}