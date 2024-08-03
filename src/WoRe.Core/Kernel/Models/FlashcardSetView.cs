using WoRe.Core.Domain.ValueObjects;

namespace WoRe.Core.Kernel.Models;

public class FlashcardSetView
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public List<FlashcardView> Flashcards { get; set; }

    public IEnumerable<FlashcardView> StilLearning
        => GetByStatus(Status.StillLearning);

    public IEnumerable<FlashcardView> Mastered
        => GetByStatus(Status.Mastered);

    public IEnumerable<FlashcardView> NotStudied
        => GetByStatus(Status.NotStudied);

    private IEnumerable<FlashcardView> GetByStatus(Status status)
        => Flashcards.Where(f => f.Status.Value == status.Value);
}