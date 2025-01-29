using marketplace_api.Data;
using marketplace_api.Interfaces;
using marketplace_api.Models;
using Microsoft.EntityFrameworkCore;

namespace marketplace_api.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _context;
    
    public CommentRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Comment>> GetAllAsync()
    {
        return await _context.Comments.ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(Guid id)
    {
        return await _context.Comments.FirstOrDefaultAsync(i => i.Id == id);
    }
}