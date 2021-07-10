using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Persistence.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectPurchaseOrder> ProjectPurchaseOrders { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(x => x.GetProperties())
                .Where(x => x.ClrType == typeof(decimal) || x.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }

            builder.Entity<Customer>(entity =>
            {
                entity.HasOne(x => x.Country)
                    .WithMany()
                    .HasForeignKey(x => x.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            builder.Entity<ProjectPurchaseOrder>(entity =>
            {
                entity.HasKey(x => new { x.ProjectId, x.PurchaseOrderId });

                entity.HasOne(x => x.Project)
                    .WithMany()
                    .HasForeignKey(x => x.ProjectId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.PurchaseOrder)
                    .WithMany()
                    .HasForeignKey(x => x.PurchaseOrderId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}