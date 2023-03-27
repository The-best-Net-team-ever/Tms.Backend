namespace Tms.Backend.Entities;

public class Cart
{
    public int Id { get; set; }
    public List<ProductVariation> ProductVariations { get; set; } = new();
}