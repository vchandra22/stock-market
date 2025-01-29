using marketplace_api.Dtos.Comment;

namespace marketplace_api.Dtos.Stock;

public class StockDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public string Symbol { get; set; } = string.Empty;
    
    public string CompanyName { get; set; } = string.Empty;
    
    public decimal Purchase { get; set; }
    
    public decimal LastDiv { get; set; }

    public string Industry { get; set; } = string.Empty;

    public long MarketCap { get; set; }
    
    public List<CommentDto> Comments { get; set; }
}