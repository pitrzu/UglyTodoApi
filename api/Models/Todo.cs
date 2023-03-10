using api.Models.ValueObjects;

namespace api.Models;

public class Todo : Entity<TodoId> {
    [Obsolete("DO NOT USE THAT EF USAGE ONLY!")]
    public Todo(
        TodoId id,
        UserId creator,
        Name name,
        Content content) : base(id) {
        Creator = creator;
        Name = name;
        Content = content;
    }
    
    private Todo(
        TodoId id,
        UserId creator,
        Name name,
        Content content,
        Category? category = null) : base(id) 
    {
        Creator = creator;
        Name = name;
        Content = content;
        Category = category ?? Category.NotCategorized;
    }
    
    public UserId Creator { get; }
    public Name Name { get; set; }
    public Content Content { get; set; }
    public DateTime CreatedAt { get; } = DateTime.Now;
    public Category Category { get; set; }

    public static Todo Create(UserId creator,
        Name name,
        Content content,
        Category? category) {
        return new Todo(TodoId.CreateUnique(), name: name, creator: creator, content: content, category: category);
    }
}

public class TodoId : ValueObj
{
    private TodoId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static TodoId Create(Guid value) => new(value);
    public static TodoId CreateUnique() => new(Guid.NewGuid());

    public static implicit operator Guid(TodoId obj) => obj.Value;
    public static implicit operator TodoId(Guid value) => new(value);
}