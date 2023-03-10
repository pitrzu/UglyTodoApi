using api.Models;
using api.Services;
using MediatR;

namespace api.Events; 

public class GetUsersTodosQueryHandler : IRequestHandler<GetUsersTodosQuery, IEnumerable<Todo>> {
    private readonly ITodoService _todoService;
    
    public GetUsersTodosQueryHandler(ITodoService todoService) {
        _todoService = todoService;
    }

    public async Task<IEnumerable<Todo>> Handle(GetUsersTodosQuery request, CancellationToken cancellationToken) {
        var todos = await _todoService.All();

        return todos.Where(todo => todo.Creator.Equals(TodoId.Create(request.Id)));
    }
}