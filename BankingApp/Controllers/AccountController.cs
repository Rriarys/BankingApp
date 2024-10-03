using BankingApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Controllers
{
    // Контроллер для управления регистрацией и выходом из системы
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<AccountController> _logger;

        // Конструктор контроллера
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        // Возвращает форму регистрации
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Обрабатывает POST-запрос для регистрации
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            _logger.LogInformation("Attempting to create user with email: {Email}", model.Email);

            if (!ModelState.IsValid)
            {
                // Логируем ошибки
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogError("ModelState Error: {ErrorMessage}", error.ErrorMessage);
                }
                return View(model); // возвращаем модель с ошибками
            }

            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                _logger.LogInformation("Trying to create user: {Email}", model.Email); // Лог

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model); // возвращаем модель с ошибками
                }

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created successfully: {Email}", model.Email); // Лог
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        // Выход из системы
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
