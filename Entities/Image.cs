namespace Tms.Backend.Entities;

public class Image
{
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public string ImageDescription { get; set; }
    
    public ProductVariation Variation { get; set; }
}