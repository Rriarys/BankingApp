using BankingApp.Context;
using BankingApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    public class BankAccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        // Конструктор контроллера
        public BankAccountController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Возвращает форму для создания банковского счета
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Обрабатывает POST-запрос для создания нового банковского счета
        [HttpPost]
        public async Task<IActionResult> Create(string currency)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var account = new BankAccount
                {
                    AccountNumber = Guid.NewGuid().ToString(),  // Генерация уникального номера счета
                    Balance = 0,  // Начальный баланс
                    Currency = currency,
                    UserId = user.Id  // Связывание счета с пользователем
                };

                _context.BankAccounts.Add(account);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}
