using Microsoft.EntityFrameworkCore;
using Tms.Backend.Entities;

namespace Tms.Backend;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Category> Categories { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductVariation> ProductVariations { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Size> Sizes { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Image>()
            .HasOne(c => c.ProductVariation)
            .WithOne(c => c.Image).HasForeignKey<Image>(x => x.ProductVariationId);
        
        modelBuilder.Entity<ProductVariation>()
            .Property(pv => pv.Price)
            .HasPrecision(18, 2); 
        
        modelBuilder.Entity<ProductVariation>()
            .HasOne(pv => pv.Product)
            .WithMany(p => p.Variations)
            .HasForeignKey(pv => pv.ProductId)
            .OnDelete(DeleteBehavior.NoAction);        
        
        modelBuilder.Entity<Category>()
            .HasOne(c => c.ParentCategory)
            .WithMany(c => c.Subcategories)
            .HasForeignKey(c => c.ParentCategoryId)
            .IsRequired(false);
        
        modelBuilder.Entity<Category>()
            .Property(c => c.Name)
            .IsRequired();
    }
}