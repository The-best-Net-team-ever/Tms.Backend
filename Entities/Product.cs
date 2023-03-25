namespace Tms.Backend.Entities;

public class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    
    public Category Category { get; set; }
    public ICollection<ProductVariation> Variations { get; set; }
    public ICollection<Review> Reviews { get; set; }
}