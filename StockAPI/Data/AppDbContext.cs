using Microsoft.EntityFrameworkCore;
using StockAPI.Models;

namespace StockAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<StockMovement> StockMovements => Set<StockMovement>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ---------------------
            // CATEGORY
            // ---------------------
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");

                entity.HasKey(c => c.Id);

                entity.Property(c => c.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.HasMany(c => c.Products)
                      .WithOne(p => p.Category)
                      .HasForeignKey(p => p.CategoryId)
                      .OnDelete(DeleteBehavior.Cascade); // choix: cascade ou restrict
            });

            // ---------------------
            // PRODUCT
            // ---------------------
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Name)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(p => p.Description)
                      .HasMaxLength(255);

                // Stock calculé par tes mouvements, mais si tu veux initialiser
                entity.Property(p => p.Stock)
                      .HasDefaultValue(0);
            });

            // ---------------------
            // STOCK MOVEMENT
            // ---------------------
            modelBuilder.Entity<StockMovement>(entity =>
            {
                entity.ToTable("StockMovements");

                entity.HasKey(sm => sm.Id);

                entity.Property(sm => sm.Type)
                      .IsRequired()
                      .HasMaxLength(10);

                entity.Property(sm => sm.Date)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP"); // pour SQL Server ou sqlite

                entity.HasOne(sm => sm.Product)
                      .WithMany()
                      .HasForeignKey(sm => sm.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
