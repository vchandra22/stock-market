namespace marketplace_api.Models;

public class Comment
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;
    
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    
    public int? StockId { get; set; }

    public Stock? Stock { get; set; }
}