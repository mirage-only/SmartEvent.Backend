using Microsoft.EntityFrameworkCore;
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
}