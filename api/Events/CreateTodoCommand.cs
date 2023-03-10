using api.Models;
using MediatR;

namespace api.Events;

public class TodoDto {
    public string Name { get; set; }
    public string Content { get; set; }
}

public class CreateTodoCommand : IRequest<Todo> {
    public string Creator { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
}