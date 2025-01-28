using marketplace_api.Data;
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
    
    public Task<List<Stock>> GetAllAsync()
    {
        return _context.Stocks.ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(Guid id)
    {
        return await _context.Stocks.FindAsync(id);
    }

    public async Task AddAsync(Stock stock)
    {
        await _context.Stocks.AddAsync(stock);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var stock = await GetByIdAsync(id);
        if (stock is not null)
        {
            _context.Stocks.Remove(stock);
        }
    }
}