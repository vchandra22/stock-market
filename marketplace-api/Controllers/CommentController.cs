using marketplace_api.Data;
using marketplace_api.Interfaces;
using marketplace_api.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace marketplace_api.Controllers;

[Route("api/v1/comments")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;

    public CommentController(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var comments = await _commentRepository.GetAllAsync();

        var commentDto = comments.Select(s => s.ToCommentDto());
        
        return Ok(commentDto);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);

        if (comment is null)
        {
            return NotFound();
        }
        
        return Ok(comment.ToCommentDto());
    }
}