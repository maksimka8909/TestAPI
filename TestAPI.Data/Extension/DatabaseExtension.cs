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
        modelBuilder.Entity<Client>().Property(c => c.CreatedAt);
        modelBuilder.Entity<Client>().Property(c => c.UpdatedAt);
        modelBuilder.Entity<Client>().Property(c => c.DeletedAt);
    }

    public static void FounderConfigure(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Founder>().ToTable("Founders");
        modelBuilder.Entity<Founder>().HasKey(f => f.Id);
        modelBuilder.Entity<Founder>().Property(f => f.Id);
        modelBuilder.Entity<Founder>().Property(f => f.Fullname).HasMaxLength(255).IsRequired();
        modelBuilder.Entity<Founder>().HasIndex(f => f.TaxpayerNumber).IsUnique();
        modelBuilder.Entity<Founder>().Property(f => f.TaxpayerNumber).HasMaxLength(12).IsRequired();
        modelBuilder.Entity<Founder>().Property(f => f.CreatedAt);
        modelBuilder.Entity<Founder>().Property(f => f.UpdatedAt);
        modelBuilder.Entity<Founder>().Property(f => f.DeletedAt);
    }

    public static IServiceCollection AddDataService(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddTransient<ISaveRepository, SaveRepository>();
        serviceCollection.AddTransient<IClientRepository, ClientRepository>();
        serviceCollection.AddTransient<IFounderRepository, FounderRepository>();
        serviceCollection.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        serviceCollection.AddTransient<ClientUseCase>();
        serviceCollection.AddTransient<FounderUseCase>();

        serviceCollection.AddDbContext<DatabaseContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return serviceCollection;
    }
}