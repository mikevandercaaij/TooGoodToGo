using System.ComponentModel.DataAnnotations;

namespace Portal.ExtensionMethods
{
    public class DateOfBirthAgeCheck : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var student = (StudentRegisterModel)validationContext.ObjectInstance;

            if(student.DateOfBirth!.Value!.AddYears(16) > DateTime.Now)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }
    }
}
