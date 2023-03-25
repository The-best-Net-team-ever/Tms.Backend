namespace Tms.Backend.Entities;

public class ProductVariation
{
    public int Id { get; set; }
    public decimal Price { get; set; }

    public Size Size { get; set; }
    public Color Color { get; set; }
    public Image Image { get; set; }
    public Product Product { get; set; }
}