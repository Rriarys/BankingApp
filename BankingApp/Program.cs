using BankingApp.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace BankingApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            // Добавление контекста базы данных с использованием SQLite
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Добавление Identity для управления пользователями и ролями
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Добавляем поддержку MVC
            builder.Services.AddControllersWithViews();

            // Настройка Serilog
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("Logs/BankingApp-{Date}.txt", rollingInterval: RollingInterval.Hour)
                .CreateLogger();

            builder.Logging.ClearProviders(); // Очистка стандартных провайдеров логирования
            builder.Logging.AddSerilog(); // Добавление Serilog


            var app = builder.Build();

            // Конфигурация HTTP-запросов
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            //app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}");

            app.Run();
        }
    }
}
