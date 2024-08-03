using WoRe.Core.Domain.Interfaces;

namespace WoRe.Core.Domain.Entities;

public class FlashcardSet : Identity, IAuditable
{
    public string Title { get; set; }
    public string? Description { get; set; } = string.Empty;
    public List<CommonCard> Cards { get; private set; } = new();

    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; set; }

    public void UpdateCards(List<CommonCard> cards)
    {
        if (cards.Count == 0)
            throw new ArgumentException("Cards empty");
        Cards = cards;
    }
}