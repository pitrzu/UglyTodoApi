using api.Models;
using MediatR;

namespace api.Events; 

public class GetUsersTodosQuery : IRequest<IEnumerable<Todo>> {
    public Guid Id { get; set; }
}