namespace Tms.Backend.Entities;

public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public string MetaData { get; set; }
    
    public Brand? Brand { get; set; }
    public int BrandId { get; set; }
    public Color? PrimaryColor { get; set; }
    public int PrimaryColorId { get; set; }
    public Category? Category { get; set; }
    public int CategoryId { get; set; }
    public ICollection<ProductVariation>? Variations { get; set; }
    public ICollection<Review>? Reviews { get; set; }
}