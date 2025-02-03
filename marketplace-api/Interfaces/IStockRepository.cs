using marketplace_api.Dtos.Stock;
using marketplace_api.Helpers;
using marketplace_api.Models;

namespace marketplace_api.Interfaces;

public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync(QueryObject query);
    Task<Stock?> GetByIdAsync(Guid id);
    Task<Stock> GetBySymbolAsync(string symbol);
    Task<Stock> CreateAsync(Stock stockModel);
    Task <Stock?> UpdateAsync(Guid id, UpdateStockRequestDto stockRequestDto);
    Task<Stock> DeleteAsync(Guid id);
    Task<bool> StockExists(Guid id);
}