using api.Models;
using api.Models.ValueObjects;
using api.Services;
using MediatR;
using OneOf.Monads;

namespace api.Events; 

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<ArgumentException, User>> {
    private readonly IUserService _userService;

    public RegisterUserCommandHandler(IUserService userService) {
        _userService = userService;
    }

    public async Task<Result<ArgumentException, User>> Handle(RegisterUserCommand request, CancellationToken cancellationToken) {
        var name = Name.Create(request.User.Login);
        var password = Password.Create(request.User.Password);
        if (name.IsError() || password.IsError())
            return new ArgumentException();

        var user = User.Create(name.SuccessValue(), Email.Create("asdasd").SuccessValue(), password.SuccessValue(), Role.User);
        await _userService.RegisterUser(user);
        
        return user;
    }
}