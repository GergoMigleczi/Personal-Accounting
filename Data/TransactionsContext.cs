using System;
using Microsoft.EntityFrameworkCore;
using PersonalAccounting.Models;
namespace PersonalAccounting.Data
{
    public class TransactionsContext : DbContext
    {
        public TransactionsContext(DbContextOptions<TransactionsContext> options) : base(options)
        {

        }

        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<Stocks> Stocks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transactions>().ToTable("Transactions");
            modelBuilder.Entity<Stocks>().ToTable("Stocks");
        }

    }
}
