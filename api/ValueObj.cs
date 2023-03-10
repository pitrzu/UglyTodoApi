namespace api;

public abstract class ValueObj : IEquatable<ValueObj>
{
    protected abstract IEnumerable<object?> GetEqualityComponents();

    public bool Equals(ValueObj? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;

        return GetEqualityComponents()
            .SequenceEqual(other.GetEqualityComponents());
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ValueObj)obj);
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(obj => obj is not null ? obj.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }

    public static bool operator ==(ValueObj? left, ValueObj? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(ValueObj? left, ValueObj? right)
    {
        return !Equals(left, right);
    }
}