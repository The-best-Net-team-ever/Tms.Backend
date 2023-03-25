namespace Tms.Backend.Entities;

public class Review
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int Rating { get; set; }
    public string Text { get; set; }
    public DateTime CreatingDate { get; set; }
}