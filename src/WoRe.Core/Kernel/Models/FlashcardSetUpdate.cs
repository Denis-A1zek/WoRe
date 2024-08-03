namespace WoRe.Core.Kernel.Models;

public record FlashcardSetUpdate : FlashcardSetCreate
{
    public FlashcardSetUpdate(Guid id, FlashcardSetCreate flashcardSetCreate) : base(flashcardSetCreate)
    {
        Id = id;
    }

    public FlashcardSetUpdate(
        Guid id,
        string title,
        string? description,
        List<FlashcardCreate> flashcards) : base(title, description, flashcards)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
