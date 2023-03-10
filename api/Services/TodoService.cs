using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public interface ITodoService {
    public Task<Todo?> ById(TodoId id);
    public Task<IEnumerable<Todo>> All();
    public Task Add(Todo todo);
    public Task Remove(Todo todo);
    public Task Update(TodoId id, Todo todo);
}

public class TodoService : ITodoService {
    private readonly TodoContext _context;
    
    public TodoService(TodoContext context) {
        _context = context;
    }


    public async Task<Todo?> ById(TodoId id) {
        return await _context.Todos.FindAsync(id);
    }

    public async Task<IEnumerable<Todo>> All() {
        return await _context.Todos.ToListAsync();
    }

    public async Task Add(Todo todo) {
        await _context.Todos.AddAsync(todo);
        await _context.SaveChangesAsync();
    }

    public async Task Remove(Todo todo) {
        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();
    }

    public async Task Update(TodoId id, Todo todo) {
        if (todo.Id != id) return;
        _context.Todos.Entry(todo).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}