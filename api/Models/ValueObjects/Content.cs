using OneOf.Monads;

namespace api.Models.ValueObjects;

public class Content : ValueObj {
    public const int MaxSize = 256;
    private Content(string value) {
        Value = value;
    }
    
    public string Value { get; }

    protected override IEnumerable<object?> GetEqualityComponents() {
        yield return Value;
    }

    public static Result<ArgumentException, Content> Create(string value) {
        if (string.IsNullOrWhiteSpace(value))
            return new ArgumentException("Content can not be empty or whitespace!");
        if (value.Length > MaxSize)
            return new ArgumentException($"Content can not be longer than {MaxSize} chars! Is: {value.Length}");

        return new Content(value);
    }
}