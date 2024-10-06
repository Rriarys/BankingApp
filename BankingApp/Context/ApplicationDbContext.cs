using BankingApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Context
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Добавляем DbSet для управления банковскими счетами
        public DbSet<BankAccount> BankAccounts { get; set; }

        // Добавляем DbSet для управления дополнительной информацией о пользователях
        public DbSet<UserAdditionalInfo> UserAdditionalInfos { get; set; } // Новая строка
    }
}
