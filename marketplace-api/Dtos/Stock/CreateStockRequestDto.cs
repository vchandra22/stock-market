using System.ComponentModel.DataAnnotations;

namespace marketplace_api.Dtos.Stock;

public class CreateStockRequestDto
{
    [Required]
    [MaxLength(10, ErrorMessage = "Content must be no more than 10 characters long.")]
    public string Symbol { get; set; } = string.Empty;
    [Required]
    [MaxLength(100, ErrorMessage = "Content must be no more than 100 characters long.")]
    public string CompanyName { get; set; } = string.Empty;
    [Required]
    [Range(1, 1000000000)]
    public decimal Purchase { get; set; }
    [Required]
    [Range(0.001, 100)]
    public decimal LastDiv { get; set; }
    [Required]
    [MaxLength(10, ErrorMessage = "Content must be no more than 10 characters long.")]
    public string Industry { get; set; } = string.Empty;
    [Range(1, 5000000000)]
    public long MarketCap { get; set; }
}