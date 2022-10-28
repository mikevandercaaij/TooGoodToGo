using System.ComponentModel.DataAnnotations;

namespace Core.Domain.ExtensionMethods
{
    public class DateOfBirthAttribute : ValidationAttribute
    {
        public DateTime DateOfBirth { get; }

        public DateOfBirthAttribute(DateTime dateOfBirth)
        {
            DateOfBirth = dateOfBirth;
        }
        
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var student = (Student)validationContext.ObjectInstance;

            if(student.DateOfBirth!.Value!.AddYears(16) > DateTime.Now)
            {
                return new ValidationResult("Je moet minimaal 16 jaar zijn om te registeren als student!");
            }

            return ValidationResult.Success;
        }
    }
}
