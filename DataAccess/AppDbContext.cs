using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public partial class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Account>? Accounts { get; set; }
        public DbSet<Transaction>? Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Account>( entity =>
            { 
                entity.HasKey(e => e.Id);
                entity.ToTable("Accounts");
            });
            modelBuilder.Entity<Transaction>(entity =>
           {
               entity.HasKey(e => e.Id);
               entity.HasOne(e => e.Account).WithMany(p => p.Transactions)
               .HasForeignKey(p => p.Id)
               .OnDelete(DeleteBehavior.Cascade)
               .HasConstraintName("FK_Transaction_Account");
               entity.ToTable("Transactions");
           });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
