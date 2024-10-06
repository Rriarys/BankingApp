using Microsoft.AspNetCore.Identity;

namespace BankingApp.Models
{
    public class UserAdditionalInfo
    {
        public int Id { get; set; } // Уникальный идентификатор

        public string FirstName { get; set; } // Имя
        public string LastName { get; set; } // Фамилия
        public string PassportNumber { get; set; } // Номер паспорта
        public string TaxpayerId { get; set; } // Номер карточки налогоплательщика
        public DateTime DateOfBirth { get; set; } // Дата рождения

        public string UserId { get; set; } // Внешний ключ для связи с Identity
        public IdentityUser User { get; set; } // Связанный объект пользователя
    }
}
