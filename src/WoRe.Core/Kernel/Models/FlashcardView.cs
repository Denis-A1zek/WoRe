using WoRe.Core.Domain.ValueObjects;

namespace WoRe.Core.Kernel.Models;

public class FlashcardView
{
    public Guid Id { get; set; }
    public string Term { get; set; }
    public string? Extra { get; set; }
    public string Definition { get; set; }
    public Status Status { get; set; }
    public string? ImageUrl { get; set; }
}