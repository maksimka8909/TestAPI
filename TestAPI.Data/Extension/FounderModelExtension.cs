using Microsoft.EntityFrameworkCore;
using TestAPI.Domain.Models;

namespace TestAPI.Data.Extension;

public static class FounderModelExtension
{
    public static void FounderConfigure(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Founder>().ToTable("founders");
        modelBuilder.Entity<Founder>().HasKey(f => f.Id);
        modelBuilder.Entity<Founder>().Property(f => f.Id).HasColumnName("id");
        modelBuilder.Entity<Founder>().Property(f => f.Fio).HasColumnName("fio").HasMaxLength(255).IsRequired();
        modelBuilder.Entity<Founder>().HasIndex(f => f.Inn).IsUnique();
        modelBuilder.Entity<Founder>().Property(f => f.Inn).HasColumnName("inn").HasMaxLength(12).IsRequired();
        modelBuilder.Entity<Founder>().Property(f => f.CreatedAt).HasColumnName("created_at")
            .HasDefaultValueSql("GETDATE()");
        modelBuilder.Entity<Founder>().Property(f => f.UpdatedAt).HasColumnName("updated_at")
            .HasDefaultValueSql("NULL");
        modelBuilder.Entity<Founder>().Property(f => f.DeletedAt).HasColumnName("deleted_at")
            .HasDefaultValueSql("NULL");
    }
}