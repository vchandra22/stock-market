using marketplace_api.Data;
using marketplace_api.Dtos.Stock;
using marketplace_api.Helpers;
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
    
    public async Task<List<Stock>> GetAllAsync(QueryObject query)
    {
        var stocks = _context.Stocks.Include(c => c.Comments).AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.CompanyName))
        {
            stocks = stocks.Where(s => s.CompanyName.Contains(query.CompanyName));
        }

        if (!string.IsNullOrWhiteSpace(query.Symbol))
        {
            stocks = stocks.Where(s => s.Symbol.Contains(query.Symbol));
        }
        
        return await stocks.ToListAsync();
    }
    
    public async Task<Stock?> GetByIdAsync(Guid id)
    {
        return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
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
        
        _context.Comments.RemoveRange(stockModel.Comments);
        
        _context.Stocks.Remove(stockModel);
        await _context.SaveChangesAsync();
        
        return stockModel;
    }

    public Task<bool> StockExists(Guid id)
    {
        return _context.Stocks.AnyAsync(i => i.Id == id);
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