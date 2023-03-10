using api.Events;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers; 

[Route("[Controller]")]
[ApiController]
public class UserController {
    private readonly IMediator _mediator;
    
    public UserController(IMediator mediator) {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost("/login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] UserLoginModel model) {
        var result = await _mediator.Send(new GenerateJwtCommand {
            User = model
        });

        return result.Match<IActionResult>(
            exception => new BadRequestObjectResult(exception.Value),
            success => new OkObjectResult(success.Value));
    }

    [AllowAnonymous]
    [HttpPost("/register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] UserLoginModel model) {
        var result = await _mediator.Send(new RegisterUserCommand {
            User = model
        });

        return result.Match<IActionResult>(
            error => new BadRequestObjectResult(error),
            success => new CreatedAtRouteResult(success.Value, success.Value.Id.ToString()));
    }
}