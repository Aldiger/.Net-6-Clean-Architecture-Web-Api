using System.Reflection;
using WebApi.Core.Entities;
using WebApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApi.Infrastructure.Configurations;

namespace WebApi.Infrastructure.Context;

public class WebApiContext : DbContext
{
    public WebApiContext(DbContextOptions<WebApiContext> options) : base(options){}


    public DbSet<Product> Products { get; set; }

    public override int SaveChanges()
    {
        TrackChanges();
        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        TrackChanges();
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    private void TrackChanges()
    {
        foreach (var entry in ChangeTracker.Entries().ToList())
        {
            if ((entry.Entity is not IAuditableEntity auditable)) continue;

            if (auditable == null) throw new ArgumentNullException(nameof(auditable));

            switch (entry.State)
            {
                case EntityState.Added:
                    ApplyConceptsForAddedEntity(entry);
                    break;
                case EntityState.Modified:
                    ApplyConceptsForUpdatedEntity(entry);
                    break;
                case EntityState.Deleted:
                    ApplyConceptsForDeletedEntity(entry);
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("EntityState type is not defined.");
            }
        }
    }

    protected virtual void ApplyConceptsForAddedEntity(EntityEntry entry)
    {
        if (entry.Entity is not IAuditableEntity)
        {
            return;
        }

        //entry.Reload();
        entry.Entity.As<IAuditableEntity>().IsDeleted = false;
        entry.Entity.As<IAuditableEntity>().IsActive = true;
        entry.Entity.As<IAuditableEntity>().CreatedDate = DateTime.Now;
        //entry.State = EntityState.Added;
    }

    protected virtual void ApplyConceptsForUpdatedEntity(EntityEntry entry)
    {
        if (entry.Entity is not IAuditableEntity)
        {
            return;
        }

        //entry.Reload();
        //Entry(auditable).Property(x => x.CreatedDate).IsModified = false;
        entry.Entity.As<IAuditableEntity>().IsDeleted = false;
        entry.Entity.As<IAuditableEntity>().IsActive = true;
        entry.Entity.As<IAuditableEntity>().UpdatedDate = DateTime.Now;
        //entry.State = EntityState.Modified;
    }

    protected virtual void ApplyConceptsForDeletedEntity(EntityEntry entry)
    {
        if (entry.Entity is not IAuditableEntity)
        {
            return;
        }

        entry.Reload();
        entry.Entity.As<IAuditableEntity>().IsDeleted = true;
        entry.Entity.As<IAuditableEntity>().IsActive = false;
        entry.Entity.As<IAuditableEntity>().UpdatedDate = DateTime.Now;
        entry.State = EntityState.Modified;
    }
}