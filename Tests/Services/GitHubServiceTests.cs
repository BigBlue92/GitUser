using System.Net;
using Domain.Models;
using Services.Services;
using Xunit;

namespace Tests.Services;

public class GitHubServiceTests
{
    [Fact]
    public async Task GetUserAsync_ReturnsUser_WhenFound()
    {
        var expected = new GitHubUser("octocat", "The Octocat", "https://avatar", "https://github.com/octocat", "bio", 2, 3, 4);
        var json = System.Text.Json.JsonSerializer.Serialize(expected);

        var handler = new StubHandler(json, HttpStatusCode.OK);
        var http = new HttpClient(handler) { BaseAddress = new Uri("https://api.github.com/") };

        var svc = new GitHubService(http);
        var result = await svc.GetUserAsync("octocat");

        Assert.NotNull(result);
        Assert.Equal("octocat", result!.Login);
        Assert.Equal(expected.Name, result.Name);
    }

    private class StubHandler : DelegatingHandler
    {
        private readonly string _content;
        private readonly HttpStatusCode _status;
        public StubHandler(string content, HttpStatusCode status)
        {
            _content = content;
            _status = status;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var resp = new HttpResponseMessage(_status)
            {
                Content = new StringContent(_content, System.Text.Encoding.UTF8, "application/json")
            };
            return Task.FromResult(resp);
        }
    }
}