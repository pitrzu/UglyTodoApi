using OneOf.Monads;

namespace api.Models.ValueObjects;

public class Name : ValueObj {
    public const int MaxSize = 32;
    
    private Name(string value) {
        Value = value;
    }

    public string Value { get; }
    
    protected override IEnumerable<object?> GetEqualityComponents() {
        yield return Value;
    }

    public static Result<ArgumentException, Name> Create(string value) {
        if (string.IsNullOrWhiteSpace(value))
            return new ArgumentException("Name can not be empty or whitespace!");
        if (value.Length > MaxSize)
            return new ArgumentException($"Name can not be longer than 32 chars! Is: {value.Length}");
        
        return new Name(value);
    }
}