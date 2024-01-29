using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestAPI.Domain.Models;

namespace TestAPI.Data.Extension;

public static class DatabaseExtension
{
    public static void ClientConfigure(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>().ToTable("Clients");
        modelBuilder.Entity<Client>()
            .HasMany(c => c.Founders)
            .WithMany(f => f.Clients)
            .UsingEntity(j => j.ToTable("ListOfClientFounders"));
        modelBuilder.Entity<Client>().HasKey(c => c.Id);
        modelBuilder.Entity<Client>().Property(c => c.Id);
        modelBuilder.Entity<Client>().Property(c => c.Name).HasMaxLength(255).IsRequired();
        modelBuilder.Entity<Client>().HasIndex(c => c.TaxpayerNumber).IsUnique();
        modelBuilder.Entity<Client>().Property(c => c.TaxpayerNumber).HasMaxLength(12).IsRequired();
        modelBuilder.Entity<Client>().Property(c => c.CreatedAt).HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<Client>().Property(c => c.UpdatedAt).HasDefaultValueSql("NULL");
        modelBuilder.Entity<Client>().Property(c => c.DeletedAt).HasDefaultValueSql("NULL");
    }

    public static void FounderConfigure(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Founder>().ToTable("Founders");
        modelBuilder.Entity<Founder>().HasKey(f => f.Id);
        modelBuilder.Entity<Founder>().Property(f => f.Id);
        modelBuilder.Entity<Founder>().Property(f => f.Fullname).HasMaxLength(255).IsRequired();
        modelBuilder.Entity<Founder>().HasIndex(f => f.TaxpayerNumber).IsUnique();
        modelBuilder.Entity<Founder>().Property(f => f.TaxpayerNumber).HasMaxLength(12).IsRequired();
        modelBuilder.Entity<Founder>().Property(f => f.CreatedAt).HasDefaultValue(DateTime.Now);
        modelBuilder.Entity<Founder>().Property(f => f.UpdatedAt).HasDefaultValueSql("NULL");
        modelBuilder.Entity<Founder>().Property(f => f.DeletedAt).HasDefaultValueSql("NULL");
    }

    public static IServiceCollection AddDataBaseConnection(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddDbContext<DatabaseContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        return serviceCollection;
    }
}