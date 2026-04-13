using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team1_CryptoCore.Domain.Models;

namespace Team1_CryptoCore.Infrastructure.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            
        }

        public DbSet<FavouriteCoin> FavouriteCoins { get; set; }

        // For Cron Job
        public DbSet<CoinMarket> Coins { get; set; }

        //Cron Job
        //public DbSet<CoinMarket> CoinMarket { get; set; }



        //  User Setting
        public DbSet<UserSetting> UserSettings { get; set; }


        public DbSet<User> User { get; set; }

        public DbSet<CryptoCoin> CryptoCoin { get; set; }

        public DbSet<Wallet> Wallet { get; set; }

        public DbSet<Transaction> Transaction { get; set; }

        public DbSet<Wishlist> Wishlists { get; set; }
          
        public DbSet<BankDetails>BankDetails { get; set; }

        public DbSet<WalletTransaction> WalletTransaction { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Wallet)
                .WithMany()
                .HasForeignKey(t => t.WalletId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Wallet>()
                .HasOne(w => w.User)
                .WithMany()
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<WalletTransaction>()
                .HasOne(wt => wt.Wallet)
                .WithMany(w => w.WalletTransaction)
                .HasForeignKey(wt => wt.WalletId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<WalletTransaction>()
                .HasOne(wt => wt.User)
                .WithMany()
                .HasForeignKey(wt => wt.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<WalletTransaction>()
                .HasOne(wt => wt.Transaction)
                .WithMany()
                .HasForeignKey(wt => wt.TransactionId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
