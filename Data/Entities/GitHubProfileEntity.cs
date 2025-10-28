using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

[Table("GitHubProfiles")]
public class GitHubProfileEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Login { get; set; } = null!;

    [MaxLength(200)]
    public string? Name { get; set; }

    public string? AvatarUrl { get; set; }
    public string? HtmlUrl { get; set; }
    public string? Bio { get; set; }

    public int PublicRepos { get; set; }
    public int Followers { get; set; }
    public int Following { get; set; }

    public DateTime QueriedAtUtc { get; set; } = DateTime.UtcNow;
}