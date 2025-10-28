using System.Text.Json.Serialization;

namespace Domain.Models;

public sealed record GitHubUser(
    [property: JsonPropertyName("login")] string Login,
    [property: JsonPropertyName("name")] string? Name,
    [property: JsonPropertyName("avatar_url")] string? Avatar_url,
    [property: JsonPropertyName("html_url")] string? Html_url,
    [property: JsonPropertyName("bio")] string? Bio,
    [property: JsonPropertyName("public_repos")] int Public_repos,
    [property: JsonPropertyName("followers")] int Followers,
    [property: JsonPropertyName("following")] int Following
);
