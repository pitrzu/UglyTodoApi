using OneOf.Monads;

namespace api.Models.ValueObjects; 

public class Email {
   private Email(string value) {
      Value = value;
   }
   
   public string Value { get; }

   public static Result<ArgumentException, Email> Create(string value) {
      return new Email(value);
   }
}