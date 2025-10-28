using Domain.Models;
using Services.Interfaces;
using System.Net.Http.Json;

namespace Services.Services;

public class GitHubService(HttpClient http) : IGitHubService
{
    private readonly HttpClient _http = http;

    public async Task<GitHubUser?> GetUserAsync(string username, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(username)) return null;

        using var req = new HttpRequestMessage(HttpMethod.Get, $"users/{Uri.EscapeDataString(username)}");
        req.Headers.UserAgent.ParseAdd("GitUserApp/1.0"); // GitHub requires User-Agent

        var resp = await _http.SendAsync(req, ct);
        if (resp.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<GitHubUser>(cancellationToken: ct);
    }
}
