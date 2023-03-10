using api.Models;
using api.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Mappings; 

public class UserMapping : IEntityTypeConfiguration<User> {
    public void Configure(EntityTypeBuilder<User> builder) {
        builder.ToTable("users");
        
        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id)
            .HasColumnName("user_id")
            .HasColumnOrder(0)
            .ValueGeneratedNever()
            .HasConversion(obj => obj.Value,
                value => UserId.Create(value));
        builder.Property(user => user.UserName)
            .HasColumnName("user_name")
            .HasConversion(obj => obj.Value,
                value => Name.Create(value).SuccessValue());
        builder.Property(user => user.Email)
            .HasColumnName("user_email")
            .HasConversion(obj => obj.Value,
                value => Email.Create(value).SuccessValue());
        builder.Property(user => user.Password)
            .HasColumnName("user_password")
            .HasConversion(obj => obj.Value,
                value => Password.Create(value).SuccessValue());
        builder.Property(user => user.Role)
            .HasColumnName("user_role");
    }
}