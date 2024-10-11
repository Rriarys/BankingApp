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
            
            // ���������� ��������� ���� ������ � �������������� SQLite
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

            // ���������� Identity ��� ���������� �������������� � ������
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // ��������� ��������� MVC
            builder.Services.AddControllersWithViews();

            // ��������� Serilog
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("Logs/BankingApp-{Date}.txt", rollingInterval: RollingInterval.Hour)
                .CreateLogger();

            builder.Logging.ClearProviders(); // ������� ����������� ����������� �����������
            builder.Logging.AddSerilog(); // ���������� Serilog


            var app = builder.Build();

            // ������������ HTTP-��������
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
