using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using api.Models.ValueObjects;
using api.Services;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using OneOf.Monads;

namespace api.Events; 

public class GenerateJwtCommandHandler : IRequestHandler<GenerateJwtCommand, Result<AuthenticationException, string>> {
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;
    
    public GenerateJwtCommandHandler(IUserService userService,
        IConfiguration configuration) {
        _userService = userService;
        _configuration = configuration;
    }

    public async Task<Result<AuthenticationException, string>> Handle(GenerateJwtCommand request, CancellationToken cancellationToken) {
        var login = Name.Create(request.User.Login);
        var password = Password.Create(request.User.Password);
        if (login.IsError() || password.IsError())
            return new AuthenticationException();

        var result = await _userService.TryLogin(login.SuccessValue(), password.SuccessValue());
        if (result.IsError())
            return result.ErrorValue();

        var user = result.SuccessValue();
        var issuer = _configuration.GetSection("Jwt").GetSection("Issuer").Value!;
        var audience = _configuration.GetSection("Jwt").GetSection("Audience").Value!;
        var stringKey = _configuration.GetSection("Jwt").GetSection("Key").Value!;

        var key = Encoding.ASCII.GetBytes(stringKey);

        var tokenDescriptor = new SecurityTokenDescriptor {
            Subject = new ClaimsIdentity(new[] {
                new Claim("Id", user.Id.Value.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName.Value),
                new Claim(JwtRegisteredClaimNames.Email, user.Email.Value),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(360),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);
        var stringToken = tokenHandler.WriteToken(token);

        return stringToken;
    }
}