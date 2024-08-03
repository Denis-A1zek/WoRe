namespace WoRe.Core.Kernel.Models;

public record FlashcardSetCreate
{
    public FlashcardSetCreate(FlashcardSetCreate flashcardSetCreate)
    {
        Title = flashcardSetCreate.Title;
        Description = flashcardSetCreate.Description;
        Flashcards = flashcardSetCreate.Flashcards;
    }

    public FlashcardSetCreate(string title, string? description, List<FlashcardCreate> flashcards)
    {
        Title = title;
        Description = description;
        Flashcards = flashcards;
    }

    public string Title { get; set; }
    public string? Description { get; set; }

    public List<FlashcardCreate> Flashcards { get; set; }
}
