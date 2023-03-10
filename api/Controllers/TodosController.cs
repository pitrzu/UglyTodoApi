using System.Security.Claims;
using api.Events;
using api.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers; 

[Route("[Controller]")]
[ApiController]
public class TodosController {
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _accessor;
    
    public TodosController(IMediator mediator, IHttpContextAccessor accessor) {
        _mediator = mediator;
        _accessor = accessor;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllTodos() {
        var userId = _accessor.HttpContext!.User.Claims.First(i => i.Type == "Id").Value;
        var response = await _mediator.Send(new GetUsersTodosQuery {
            Id = Guid.Parse(userId)
        });

        return response.Any() ? new OkObjectResult(response) : new EmptyResult();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Add([FromBody] TodoDto dto) {
        var userId = _accessor.HttpContext!.User.Claims.First(i => i.Type == "Id").Value;
        var response = await _mediator.Send(new CreateTodoCommand {
            Creator = userId,
            Content = dto.Content,
            Name = dto.Name
        });

        return new OkObjectResult(response);
    }
}