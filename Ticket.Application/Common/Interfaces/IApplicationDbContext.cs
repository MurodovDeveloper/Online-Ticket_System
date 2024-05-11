using Microsoft.EntityFrameworkCore;
using Ticket.Domain.Entities;
using Ticket.Domain.Entities.Identity;

namespace Ticket.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<ApplicationUser> ApplicationUsers { get; }
        DbSet<Contact> Contacts { get; }
        DbSet<Customer> Customers { get; }
        DbSet<Organization> Organizations { get; }
        DbSet<Product> Products { get; }
        DbSet<SupportAgent> SupportAgents { get; }
        DbSet<SupportEngineer> SupportEngineers { get; }
        DbSet<Tickets> Tickets { get; }
        DbSet<UserRefreshToken> RefreshTokens { get; }
        DbSet<Role> Roles { get; }
        DbSet<Permission> Permissions { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
