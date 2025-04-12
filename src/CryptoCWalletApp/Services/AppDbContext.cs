using CryptoCWalletApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace CryptoCWalletApp.Services
{
    public class AppDbContext : DbContext
    {
        public DbSet<CryptoCurrencyEntity> CryptoCurrency { get; set; }
        
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
    }
}