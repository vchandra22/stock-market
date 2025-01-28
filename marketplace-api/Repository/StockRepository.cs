using marketplace_api.Data;
using marketplace_api.Dtos.Stock;
using marketplace_api.Interfaces;
using marketplace_api.Models;
using Microsoft.EntityFrameworkCore;

namespace marketplace_api.Repository;

public class StockRepository : IStockRepository
{
    private readonly ApplicationDbContext _context;
    
    public StockRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Stock>> GetAllAsync()
    {
        return await _context.Stocks.ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(Guid id)
    {
        return await _context.Stocks.FindAsync(id);
    }

    public async Task<Stock> CreateAsync(Stock stockModel)
    {
        await _context.Stocks.AddAsync(stockModel);
        await _context.SaveChangesAsync();
        
        return stockModel;
    }
    
    public async Task<Stock> DeleteAsync(Guid id)
    {
        var stockModel = await GetByIdAsync(id);
        
        if (stockModel is null)
        {
            return null;
        }
        
        _context.Stocks.Remove(stockModel);
        await _context.SaveChangesAsync();
        
        return stockModel;
    }

    public async Task<Stock?> UpdateAsync(Guid id, UpdateStockRequestDto stockRequestDto)
    {
        var existingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

        if (existingStock is null)
        {
            return null;
        }
        
        existingStock.Symbol = stockRequestDto.Symbol;
        existingStock.CompanyName = stockRequestDto.CompanyName;
        existingStock.Purchase = stockRequestDto.Purchase;
        existingStock.LastDiv = stockRequestDto.LastDiv;
        existingStock.Industry = stockRequestDto.Industry;
        existingStock.MarketCap = stockRequestDto.MarketCap;
        
        await _context.SaveChangesAsync();
        
        return existingStock;
    }
}