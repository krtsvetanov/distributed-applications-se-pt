using Microsoft.EntityFrameworkCore;
using SIMS.WebAPI.Entities;

namespace SIMS.WebAPI.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<StockMovement> StockMovements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure self-referencing relationship for Category
        modelBuilder.Entity<Category>()
            .HasOne(c => c.ParentCategory)
            .WithMany(c => c.SubCategories)
            .HasForeignKey(c => c.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure Product relationships
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Supplier)
            .WithMany(s => s.Products)
            .HasForeignKey(p => p.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure StockMovement relationship
        modelBuilder.Entity<StockMovement>()
            .HasOne(sm => sm.Product)
            .WithMany(p => p.StockMovements)
            .HasForeignKey(sm => sm.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure decimal precision for Supplier Rating (0-5 range with 2 decimal places)
        modelBuilder.Entity<Supplier>()
            .Property(s => s.Rating)
            .HasPrecision(3, 2); // Allows values like 4.85, 3.90, etc.

        // Add indexes for better query performance
        modelBuilder.Entity<Product>()
            .HasIndex(p => p.SKU)
            .IsUnique();

        modelBuilder.Entity<Product>()
            .HasIndex(p => p.CategoryId);

        modelBuilder.Entity<Product>()
            .HasIndex(p => p.SupplierId);

        modelBuilder.Entity<StockMovement>()
            .HasIndex(sm => sm.ProductId);

        modelBuilder.Entity<StockMovement>()
            .HasIndex(sm => sm.Date);
    }
}
