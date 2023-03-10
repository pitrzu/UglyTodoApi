using System.Security.Authentication;
using api.Models;
using api.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;
using OneOf.Monads;

namespace api.Services;

public interface IUserService {
    public Task<Result<AuthenticationException, User>> TryLogin(Name login, Password password);
    public Task<User> RegisterUser(User user);
}

public class UserService : IUserService {
    private readonly TodoContext _context;

    public UserService(TodoContext context) {
        _context = context;
    }

    public async Task<Result<AuthenticationException, User>> TryLogin(Name login, Password password) {
        var users = _context.Users.Where(user => user.UserName == login && user.Password == password);
        if (await users.CountAsync() != 1)
            return new AuthenticationException();

        return await users.FirstAsync();
    }

    public async Task<User> RegisterUser(User user) {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;
    }
}