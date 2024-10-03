using Microsoft.AspNetCore.Identity;

namespace BankingApp.Models
{
    // Bank account model
    public class BankAccount
    {
        public int Id { get; set; }  // Уникальный идентификатор счета

        public string AccountNumber { get; set; }  // Сгенерированный номер счета

        public decimal Balance { get; set; }  // Баланс на счете

        public string Currency { get; set; }  // Валюта счета (UAH, USD, EUR)

        // Связь счета с пользователем
        public string UserId { get; set; }  // Внешний ключ для пользователя
        public IdentityUser User { get; set; }  // Связанный объект пользователя (IdentityUser)
    }
}
