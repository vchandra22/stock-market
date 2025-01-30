using marketplace_api.Models;

namespace marketplace_api.Interfaces;

public interface ICommentRepository
{
    Task<List<Comment>> GetAllAsync();
    Task<Comment?> GetByIdAsync(Guid id);
    Task<Comment> CreateAsync(Comment commentModel);
    Task<Comment?> UpdateAsync(Guid id, Comment commentModel);
}