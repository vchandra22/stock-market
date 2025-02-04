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
        return await _context.Comments.Include(a => a.AppUser).ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(Guid id)
    {
        return await _context.Comments.Include(a => a.AppUser).FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Comment> CreateAsync(Comment commentModel)
    {
        await _context.Comments.AddAsync(commentModel);
        await _context.SaveChangesAsync();
        
        return commentModel;
    }

    public async Task<Comment?> UpdateAsync(Guid id, Comment commentModel)
    {
        var existingComment = await _context.Comments.FindAsync(id);

        if (existingComment is null)
        {
            return null;
        }
        
        existingComment.Title = commentModel.Title;
        existingComment.Content = commentModel.Content;
        
        await _context.SaveChangesAsync();
        
        return existingComment;
    }

    public async Task<Comment?> DeleteAsync(Guid id)
    {
        var commentModel = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);

        if (commentModel is null)
        {
            return null;
        }
        
        _context.Comments.Remove(commentModel);
        await _context.SaveChangesAsync();
        
        return commentModel;
    }
}