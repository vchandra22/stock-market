using marketplace_api.Data;
using marketplace_api.Dtos.Stock;
using marketplace_api.Interfaces;
using marketplace_api.Mappers;
using Microsoft.AspNetCore.Mvc;

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
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var stocks = await _stockRepository.GetAllAsync();
        
        var stockDto = stocks.Select(s => s.ToStockDto());
        
        return Ok(stockDto);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
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
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var stockModel = stockDto.ToStockFromCreateDTO();
        
        await _stockRepository.CreateAsync(stockModel);
        
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }

    [HttpPut]
    [Route("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateStockRequestDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var stockModel = await _stockRepository.UpdateAsync(id, updateDto);

        if (stockModel is null)
        {
            return NotFound();
        }
        
        return Ok(stockModel.ToStockDto());
    }

    [HttpDelete]
    [Route("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        await _stockRepository.DeleteAsync(id);
        
        return Ok();
    }
}