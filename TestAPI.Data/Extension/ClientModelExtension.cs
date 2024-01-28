using Microsoft.EntityFrameworkCore;
using TestAPI.Domain.Models;

namespace TestAPI.Data.Extension;

public static class ClientModelExtension
{
    public static void ClientConfigure(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>().ToTable("clients");
        modelBuilder.Entity<Client>()
            .HasMany(c => c.Founders)
            .WithMany(f => f.Clients)
            .UsingEntity(j => j.ToTable("listOfClientFounders"));
        modelBuilder.Entity<Client>().HasKey(c => c.Id);
        modelBuilder.Entity<Client>().Property(c => c.Id).HasColumnName("id");
        modelBuilder.Entity<Client>().Property(c => c.Name).HasColumnName("name").HasMaxLength(255).IsRequired();
        modelBuilder.Entity<Client>().HasIndex(c => c.Inn).IsUnique();
        modelBuilder.Entity<Client>().Property(c => c.Inn).HasColumnName("inn").HasMaxLength(12).IsRequired();
        modelBuilder.Entity<Client>().Property(c => c.Type).HasColumnName("client_type").IsRequired();
        modelBuilder.Entity<Client>().Property(c => c.Type).HasConversion<string>();
        modelBuilder.Entity<Client>().Property(c => c.CreatedAt).HasColumnName("created_at")
            .HasDefaultValueSql("GETDATE()");
        modelBuilder.Entity<Client>().Property(c => c.UpdatedAt).HasColumnName("updated_at")
            .HasDefaultValueSql("NULL");
        modelBuilder.Entity<Client>().Property(c => c.DeletedAt).HasColumnName("deleted_at")
            .HasDefaultValueSql("NULL");
    }
}