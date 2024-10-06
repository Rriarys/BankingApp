using System.ComponentModel.DataAnnotations;

namespace BankingApp.Validators
{

    public class AgeValidation : ValidationAttribute
    {
        private readonly int _minimumAge;

        public AgeValidation(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateOfBirth)
            {
                var age = DateTime.Today.Year - dateOfBirth.Year;
                if (dateOfBirth > DateTime.Today.AddYears(-age)) age--;

                if (age < _minimumAge)
                {
                    return new ValidationResult($"Пользователь должен быть не моложе {_minimumAge} лет.");
                }
            }
            return ValidationResult.Success;
        }
    }
}

