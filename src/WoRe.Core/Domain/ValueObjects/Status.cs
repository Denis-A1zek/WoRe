using CSharpFunctionalExtensions;
using ErrorOr;

namespace WoRe.Core.Domain.ValueObjects;

public class Status : ValueObject
{
    public static readonly Status NotStudied = new("Not studied");
    public static readonly Status StillLearning = new("Still learning");
    public static readonly Status Mastered = new(nameof(Mastered));

    private static readonly Status[] _all = [NotStudied, StillLearning, Mastered];

    public string Value { get; }
    private Status(string value)
    {
        Value = value;
    }

    public static ErrorOr<ValueObject> Create(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return Error.Failure("Value is requiered");

        var status = input.Trim().ToLower();

        if (_all.Any(a => a.Value.ToLower() == status) == false)
            return Error.Failure("Value is invalid");

        return new Status(status);
    }

    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return NotStudied;
        yield return StillLearning;
        yield return Mastered;
    }
}