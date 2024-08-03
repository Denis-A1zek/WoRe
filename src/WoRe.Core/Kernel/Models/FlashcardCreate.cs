using System.ComponentModel.DataAnnotations;

namespace WoRe.Core.Kernel.Models;

public record FlashcardCreate
{
    public FlashcardCreate()
    {
    }

    [Required]
    public string Term { get; set; }
    [Required]
    public string Definition { get; set; }
    public string? Extra { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;
}
