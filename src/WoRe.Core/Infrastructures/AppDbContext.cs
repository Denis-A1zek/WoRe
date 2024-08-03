using Microsoft.EntityFrameworkCore;
using WoRe.Core.Domain.Entities;
using WoRe.Core.Infrastructures.Database.Interceptors;

namespace WoRe.Core.Infrastructures;

public class AppDbContext : DbContext
{
    private string _connectionString;

    public AppDbContext(DbContextOptions options)
        : base(options) { }

    public AppDbContext(string connectionString)
        => _connectionString = connectionString;

    public virtual DbSet<FlashcardSet> FlashcardSet { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.AddInterceptors(new CreatedAtInterceptor());

        if (!string.IsNullOrWhiteSpace(_connectionString))
            options.UseSqlite(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.Entity<CommonCard>()
            .HasDiscriminator<string>("WordType")
            .HasValue<CommonCard>(nameof(CommonCard))
            .HasValue<AdditionalCard>(nameof(AdditionalCard));

        modelBuilder.Entity<CommonCard>()
            .ComplexProperty(
                ap => ap.Status,
                b =>
                {
                    b.Property(p => p.Value).HasColumnName("Status");
                });

        base.OnModelCreating(modelBuilder);
    }
}