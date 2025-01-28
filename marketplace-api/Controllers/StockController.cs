using marketplace_api.Data;
using marketplace_api.Dtos.Stock;
using marketplace_api.Interfaces;
using marketplace_api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace marketplace_api.Controllers;

[Route("api/v1/stocks")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IStockRepository _stockRepository;
    
    public StockController(ApplicationDbContext context, IStockRepository stockRepository)
    {
        _stockRepository = stockRepository;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stocks = await _stockRepository.GetAllAsync();
        
        var stockDto = stocks.Select(s => s.ToStockDto());
        
        return Ok(stockDto);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var stock = await _stockRepository.GetByIdAsync(id);

        if (stock is null)
        {
            return NotFound();
        }
        
        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
    {
        var stockModel = stockDto.ToStockFromCreateDTO();
        
        await _stockRepository.AddAsync(stockModel);
        
        await _stockRepository.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateStockRequestDto updateDto)
    {
        var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

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
        
        await _stockRepository.SaveChangesAsync();
        
        return Ok(stockModel.ToStockDto());
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

        if (stockModel is null)
        {
            return NotFound();
        }

        await _stockRepository.DeleteAsync(id);
        
        await _stockRepository.SaveChangesAsync();
        
        return NoContent();
    }
}