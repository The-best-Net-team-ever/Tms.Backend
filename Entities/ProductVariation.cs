namespace Tms.Backend.Entities;

public class ProductVariation
{
    public int Id { get; set; }
    public decimal Price { get; set; }

    public Size Size { get; set; }
    public int SizeId { get; set; }
    public Color Color { get; set; }
    public int ColorId { get; set; }
    public Image Image { get; set; }
    public int ImageId { get; set; }
    public Product Product { get; set; }
    public int ProductId { get; set; }
    public List<Cart>? Carts { get; set; }
}