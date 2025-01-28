using marketplace_api.Dtos.Stock;
using marketplace_api.Models;

namespace marketplace_api.Interfaces;

public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync();
    Task<Stock?> GetByIdAsync(Guid id);
    Task AddAsync(Stock stock);
    Task SaveChangesAsync();
    Task DeleteAsync(Guid id);
}