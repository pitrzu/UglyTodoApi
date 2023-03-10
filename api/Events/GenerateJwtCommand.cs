using System.Security.Authentication;
using MediatR;
using OneOf.Monads;

namespace api.Events;

public class UserLoginModel {
    public string Login { get; set; }
    public string Password { get; set; }
}

public class GenerateJwtCommand : IRequest<Result<AuthenticationException, string>> {
    public UserLoginModel User { get; set; }
}