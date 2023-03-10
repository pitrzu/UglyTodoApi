using api.Models;
using api.Models.ValueObjects;
using api.Services;
using MediatR;

namespace api.Events;

public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, Todo> {
    private readonly ITodoService _todoService;

    public CreateTodoCommandHandler(ITodoService todoService) {
        _todoService = todoService;
    }

    public async Task<Todo?> Handle(CreateTodoCommand request, CancellationToken cancellationToken) {
        var name = Name.Create(request.Name).SuccessValue();
        var content = Content.Create(request.Content).SuccessValue();
        var creator = UserId.Create(Guid.Parse(request.Creator));

        var todo = Todo.Create(creator, name, content, null);
        await _todoService.Add(todo);
        return (await _todoService.ById(todo.Id)!);
    }
}