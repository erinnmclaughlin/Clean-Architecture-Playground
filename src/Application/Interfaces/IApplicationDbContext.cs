using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Country> Countries { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Project> Projects { get; set; }
        DbSet<ProjectPurchaseOrder> ProjectPurchaseOrders { get; set; }
        DbSet<PurchaseOrder> PurchaseOrders { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}