using marketplace_api.Models;

namespace marketplace_api.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllAsync();
    Task<Comment?> GetByIdAsync(Guid id);
}