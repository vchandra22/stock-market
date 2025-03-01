using Microsoft.AspNetCore.Identity;

namespace marketplace_api.Models;

public class AppUser : IdentityUser
{
    public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
}