using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestAPI.Data.Repositories;
using TestAPI.Domain.Interfaces;
using TestAPI.Domain.Models;
using TestAPI.Domain.UseCases;

namespace TestAPI.Data.Extension;

public static class DatabaseExtension
{
    public static void ClientConfigure(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>()
            .HasMany(c => c.Founders)
            .WithMany(f => f.Clients);
        modelBuilder.Entity<Client>().HasKey(c => c.Id);
        modelBuilder.Entity<Client>().Property(c => c.Name).HasMaxLength(255).IsRequired();
        modelBuilder.Entity<Client>().HasIndex(c => c.TaxpayerNumber).IsUnique();
        modelBuilder.Entity<Client>().Property(c => c.TaxpayerNumber).HasMaxLength(12).IsRequired();
    }

    public static void FounderConfigure(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Founder>().HasKey(f => f.Id);
        modelBuilder.Entity<Founder>().Property(f => f.Fullname).HasMaxLength(255).IsRequired();
        modelBuilder.Entity<Founder>().HasIndex(f => f.TaxpayerNumber).IsUnique();
        modelBuilder.Entity<Founder>().Property(f => f.TaxpayerNumber).HasMaxLength(12).IsRequired();
    }

    public static IServiceCollection AddDataService(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddScoped<ISaveRepository, SaveRepository>();
        serviceCollection.AddScoped<IClientRepository, ClientRepository>();
        serviceCollection.AddScoped<IFounderRepository, FounderRepository>();

        serviceCollection.AddScoped<ClientUseCase>();
        serviceCollection.AddScoped<FounderUseCase>();

        serviceCollection.AddDbContext<DatabaseContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("TestAPI.Data")));

        return serviceCollection;
    }
}