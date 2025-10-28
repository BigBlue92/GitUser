using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GitHubController(IGitHubService gitHubService, ILogger<GitHubController> logger) : ControllerBase
{
    private readonly IGitHubService _gitHubService = gitHubService;
    private readonly ILogger<GitHubController> _logger = logger;

    [HttpGet("{username}")]
    public async Task<IActionResult> Get(string username, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(username)) return BadRequest("username required");

        var user = await _gitHubService.GetUserAsync(username, ct);
        if (user is null) return NotFound();

        return Ok(user);
    }
}
