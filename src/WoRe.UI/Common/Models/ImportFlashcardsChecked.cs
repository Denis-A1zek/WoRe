using WoRe.Core.Kernel.Models;

namespace WoRe.UI.Common.Models;

public record ImportFlashcardsChecked
{
    public ImportFlashcardsChecked(FlashcardSetDetails flashcardSetDetails)
    {
        FlashcardSet = flashcardSetDetails;
        IsChecked = false;
    }
    public FlashcardSetDetails FlashcardSet { get; set; }
    public bool IsChecked { get; set; }
}
