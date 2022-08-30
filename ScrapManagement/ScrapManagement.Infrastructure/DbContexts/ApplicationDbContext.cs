using AspNetCoreHero.Abstractions.Domain;
using AspNetCoreHero.EntityFrameworkCore.AuditTrail;
using Microsoft.EntityFrameworkCore;
using ScrapManagement.Application.Interfaces.Contexts;
using ScrapManagement.Application.Interfaces.Shared;
using ScrapManagement.Domain.Entities;
using ScrapManagement.Infrastructure.Identity.Seeds;
using System;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;

namespace ScrapManagement.Infrastructure.DbContexts
{
    public class ApplicationDbContext : AuditableContext, IApplicationDbContext
    {
        private readonly IAuthenticatedUserService _authenticatedUser;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IAuthenticatedUserService authenticatedUser) : base(options)
        {
            _authenticatedUser = authenticatedUser;
        }

        public DbSet<Scrap> Scraps { get; set; }

        public IDbConnection Connection => Database.GetDbConnection();

        public bool HasChanges => ChangeTracker.HasChanges();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = DateTime.UtcNow;
                        entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                        break;
                }
            }
            if (_authenticatedUser.UserId == null)
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            else
            {
                return await base.SaveChangesAsync(_authenticatedUser.UserId);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }
            base.OnModelCreating(builder);
        }
    }
}