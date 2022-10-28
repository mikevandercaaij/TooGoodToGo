using System.ComponentModel.DataAnnotations;

namespace Portal.Tests
{
    public class TestHelper
    {
        public static IList<ValidationResult> ValidateModel(object obj)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(obj, null, null);
            Validator.TryValidateObject(obj, validationContext, validationResults, true);
            return validationResults;
        }
    }
}