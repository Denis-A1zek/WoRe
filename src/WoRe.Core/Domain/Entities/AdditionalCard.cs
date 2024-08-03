namespace WoRe.Core.Domain.Entities;

public class AdditionalCard : CommonCard
{
    public AdditionalCard(
        string term, string extra, string definition, string imageUrl = null)
            : base(term, definition, imageUrl)
    {
        Extra = extra;
    }
    public string Extra { get; set; }
}