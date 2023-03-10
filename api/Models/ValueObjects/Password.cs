using OneOf.Monads;

namespace api.Models.ValueObjects; 

public class Password : ValueObj{
   public string Value { get;}

   private Password(string value) {
      Value = value;
   }
   
   public static Result<ArgumentException, Password> Create(string value) {
      return new Password(value);
   }

   protected override IEnumerable<object?> GetEqualityComponents() {
      yield return Value;
   }
}