namespace WoRe.UI.Components.Pages.Library.Models;

public enum CircleColor
{
    Green,
    Blue,
    Orange
}

public record QuantityIndicatorArgs
{
    public QuantityIndicatorArgs(CircleColor color, int count, string title)
    {
        Color = color;
        Count = count;
        Title = title;
    }

    public  CircleColor Color { get; init; }
    public int Count { get; init; }
    public string Title { get; init; }
}