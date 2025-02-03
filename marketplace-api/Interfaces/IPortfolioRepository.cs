using marketplace_api.Models;

namespace marketplace_api.Interfaces;

public interface IPortfolioRepository
{
    Task<List<Stock>> GetUserPortfolio(AppUser user);
    Task<Portfolio> CreateAsync(Portfolio portfolio);
    Task<Portfolio> DeletePortfolio(AppUser appUser, string symbol);
}