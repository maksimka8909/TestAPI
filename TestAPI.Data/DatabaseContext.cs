using Microsoft.EntityFrameworkCore;
using TestAPI.Data.Extension;
using TestAPI.Domain.Models;

namespace TestAPI.Data;

public sealed class DatabaseContext : DbContext
{
    public DbSet<Founder> Founders { get; set; }

    public DbSet<Client> Clients { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ClientConfigure();
        modelBuilder.FounderConfigure();
        base.OnModelCreating(modelBuilder);
    }
}