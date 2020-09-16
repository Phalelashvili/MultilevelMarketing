using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MultilevelMarketing.Domain;
using MultilevelMarketing.Domain.Models;

namespace MultilevelMarketing.Infrastructure.Context
{
    public class MlmContext : DbContext
    {
        public DbSet<Distributor> Distributors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        
        public DbSet<Document> Documents { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }

        public MlmContext(DbContextOptions<MlmContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // set ON DELETE behavior of foreign keys to SetNull
            modelBuilder.Entity<Distributor>()
                .HasOne(distributor => distributor.Referrer)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<Sale>()
                .HasOne(sale => sale.Product)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<Sale>()
                .HasOne(sale => sale.Distributor)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);
        }
        
    }
}