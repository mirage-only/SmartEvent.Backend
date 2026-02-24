using Microsoft.EntityFrameworkCore;
using SmartEvent.Backend.Core.Interfaces.IModels;
using SmartEvent.Backend.Core.Models;
using SmartEvent.Backend.Persistence.Configurations;

namespace SmartEvent.Backend.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): DbContext(options)
{
    public DbSet<User>  Users { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Attendance>  Attendances { get; set; }
    public DbSet<QrCode> QrCodes { get; set; }
    public DbSet<Registration> Registrations { get; set; }
    public DbSet<EventOrganizer> EventOrganizers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new EventConfiguration());
        modelBuilder.ApplyConfiguration(new AttendanceConfiguration());
        modelBuilder.ApplyConfiguration(new EventOrganizerConfiguration());
        modelBuilder.ApplyConfiguration(new QrCodeConfiguration());
        modelBuilder.ApplyConfiguration(new RegistrationConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditableInformation();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        UpdateAuditableInformation();
        return base.SaveChanges();
    }

    private void UpdateAuditableInformation()
    {
        var modifiedEntries = ChangeTracker.Entries<IAuditableModel>();

        foreach (var entry in modifiedEntries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Property(model => model.CreatedAt).IsModified = false;
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}