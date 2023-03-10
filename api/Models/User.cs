using api.Models.ValueObjects;

namespace api.Models;

public class User : Entity<UserId>
{
    private User(
        UserId id,
        Name userName,
        Email email,
        Password password,
        Role role) : base(id) 
    {
        UserName = userName;
        Email = email;
        Password = password;
        Role = role;
    }
    
    public Name UserName { get; set; }
    public Email Email { get; set; }
    public Password Password { get; set; }
    public Role Role { get; set; }

    public static User Create(Name name, Email email, Password password, Role? role) {
        return CreateWithId(id: UserId.CreateUnique(), name: name, email: email, password: password, role: role);
    }

    public static User CreateWithId(UserId id, Name name, Email email, Password password, Role? role) {
        return new User(
            id: id,
            userName: name,
            email: email,
            password: password,
            role: role ?? Role.User);
    }
}

public class UserId : ValueObj
{
    private UserId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public static UserId Create(Guid value) => new(value);
    public static UserId CreateUnique() => new(Guid.NewGuid());

    public static implicit operator Guid(UserId obj) => obj.Value;
    public static implicit operator UserId(Guid value) => new(value);

}