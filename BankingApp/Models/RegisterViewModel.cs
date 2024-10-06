using System.ComponentModel.DataAnnotations;
using BankingApp.Validators;

namespace BankingApp.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; } // Имя

        [Required]
        public string LastName { get; set; } // Фамилия

        [Required]
        [RegularExpression(@"^[A-Za-z]{2}\d{6}$", ErrorMessage = "Номер паспорта должен состоять из 2 букв и 6 цифр.")]
        public string PassportNumber { get; set; } // Номер паспорта

        [Required]
        [RegularExpression(@"^\d{12}$", ErrorMessage = "Номер карточки налогоплательщика должен состоять из 12 цифр.")]
        public string TaxpayerId { get; set; } // Номер карточки налогоплательщика

        [Required]
        [DataType(DataType.Date)]
        [AgeValidation(18)] // Валидация возраста
        public DateTime DateOfBirth { get; set; } // Дата рождения
    }
}