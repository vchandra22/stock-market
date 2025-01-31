using marketplace_api.Models;

namespace marketplace_api.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}