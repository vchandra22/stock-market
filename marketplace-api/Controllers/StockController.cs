using marketplace_api.Data;
using marketplace_api.Dtos.Stock;
using marketplace_api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace marketplace_api.Controllers;

[Route("api/v1/stocks")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public StockController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var stocks = _context.Stocks.ToList()
                .Select(s => s.ToStockDto());
        
        return Ok(stocks);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        var stock = _context.Stocks.Find(id);

        if (stock is null)
        {
            return NotFound();
        }
        
        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
    {
        var stockModel = stockDto.ToStockFromCreateDTO();
        _context.Stocks.Add(stockModel);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }

    [HttpPut]
    [Route("{id:guid}")]
    public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateStockRequestDto updateDto)
    {
        var stockModel = _context.Stocks.FirstOrDefault(x => x.Id == id);

        if (stockModel is null)
        {
            return NotFound();
        }
        
        stockModel.Symbol = updateDto.Symbol;
        stockModel.CompanyName = updateDto.CompanyName;
        stockModel.Purchase = updateDto.Purchase;
        stockModel.LastDiv = updateDto.LastDiv;
        stockModel.Industry = updateDto.Industry;
        stockModel.MarketCap = updateDto.MarketCap;
        
        _context.SaveChanges();
        
        return Ok(stockModel.ToStockDto());
    }
}