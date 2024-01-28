using Microsoft.EntityFrameworkCore;
using TestAPI.Data.Extension;
using TestAPI.Domain.Models;

namespace TestAPI.Data;

public sealed class ApiContext : DbContext
{
    public DbSet<Founder> Founders { get; set; }

    public DbSet<Client> Clients { get; set; }

    public ApiContext(DbContextOptions<ApiContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ClientConfigure();
        modelBuilder.FounderConfigure();
        base.OnModelCreating(modelBuilder);
    }
}