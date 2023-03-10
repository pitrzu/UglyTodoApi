using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api;

public class TodoContext : DbContext {
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Todo> Todos { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseNpgsql("User ID=postgres; Password=dbPaswd123; Host=localhost; Port=5432; Database=dev_db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TodoContext).Assembly);
    }
}