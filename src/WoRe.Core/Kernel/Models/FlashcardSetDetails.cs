namespace WoRe.Core.Kernel.Models;

public record FlashcardSetDetails
{
    public Guid Id { get; set; }
    public required string Title { get; init; }
    public string? Description { get; init; }
    public int TermsCount { get; init; } = 0;
    public int StillLearningCount { get; init; } = 0;
    public int NotStudiedCount { get; init; } = 0;
    public int MasteredCount { get; init; } = 0;
}