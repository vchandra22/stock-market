using marketplace_api.Extensions;
using marketplace_api.Interfaces;
using marketplace_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace marketplace_api.Controllers;

[Route("api/v1/portfolio")]
public class PortfolioController : ControllerBase 
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IStockRepository _stockRepository;
    private readonly IPortfolioRepository _portfolioRepository;
    
    public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository)
    {
        _userManager = userManager;
        _stockRepository = stockRepository;
        _portfolioRepository = portfolioRepository;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserPortfolio()
    {
        var username = User.GetUsername();
        
        var appUser = await _userManager.FindByNameAsync(username);
        
        var portfolio = await _portfolioRepository.GetUserPortfolio(appUser);
        
        return Ok(portfolio);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddPortfolio(string symbol)
    {
        var username = User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);
        var stock = await _stockRepository.GetBySymbolAsync(symbol);

        if (stock is null)
            return BadRequest("Stock not found");
        
        var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);

        if (userPortfolio.Any(e => e.Symbol.ToLower()  == symbol.ToLower()))
            return BadRequest("Cannot add same stock to portfolio");

        var portfolioModel = new Portfolio
        {
            StockId = stock.Id,
            AppUserId = appUser.Id,
        };
        
        await _portfolioRepository.CreateAsync(portfolioModel);

        if (portfolioModel is null)
        {
            return StatusCode(500, "Could not create portfolio");
        }
        else
        {
            return Created();
        }
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeletePortfolio(string symbol)
    {
        var username = User.GetUsername();
        var appUser = await _userManager.FindByNameAsync(username);
        
        var userPortfolio = await _portfolioRepository.GetUserPortfolio(appUser);
        
        var filterStock = userPortfolio.Where(s => s.Symbol.ToLower() == symbol.ToLower().ToLower()).ToList();

        if (filterStock.Count == 1)
        {
            await _portfolioRepository.DeletePortfolio(appUser, symbol);
        }
        else
        {
            return BadRequest("Stock not in your portfolio");
        }
        
        return Ok();
    }
}