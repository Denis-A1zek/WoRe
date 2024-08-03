using WoRe.Core.Domain.ValueObjects;

namespace WoRe.Core.Domain.Entities;

public class CommonCard : Identity
{
    public CommonCard(string term, string definition, string imageUrl = null)
    {
        Term = term;
        Definition = definition;
        ImageUrl = imageUrl;
    }
    public string Term { get; init; }
    public string Definition { get; init; }
    public Status Status { get; private set; } = Status.NotStudied;
    public string? ImageUrl { get; private set; } = string.Empty;
}