using api.Models;
using MediatR;
using OneOf.Monads;

namespace api.Events; 

public class RegisterUserCommand : IRequest<Result<ArgumentException, User>> {
    public UserLoginModel User { get; set; }
}