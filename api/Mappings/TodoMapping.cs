using api.Models;
using api.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Mappings; 

public class TodoMapping : IEntityTypeConfiguration<Todo> {
    public void Configure(EntityTypeBuilder<Todo> builder) {
        builder.ToTable("todos");
        
        builder.HasKey(todo => todo.Id);
        
        builder.Property(todo => todo.Id)
            .HasColumnName("todo_id")
            .HasColumnOrder(0)
            .ValueGeneratedNever()
            .HasConversion(obj => obj.Value,
                value => TodoId.Create(value));
        builder.Property(todo => todo.Creator)
            .HasColumnName("todo_creator_id")
            .HasConversion(obj => obj.Value,
                value => UserId.Create(value));
        builder.Property(todo => todo.Name)
            .HasColumnName("todo_name")
            .HasMaxLength(Name.MaxSize)
            .HasConversion(obj => obj.Value,
                value => Name.Create(value).SuccessValue());
        builder.Property(todo => todo.Content)
            .HasColumnName("todo_content")
            .HasMaxLength(Content.MaxSize)
            .HasConversion(obj => obj.Value,
                value => Content.Create(value).SuccessValue());
        builder.Property(todo => todo.CreatedAt)
            .HasColumnName("todo_created_at");
        builder.Property(todo => todo.Category)
            .HasColumnName("todo_category");
    }
}