using marketplace_api.Data;
using marketplace_api.Dtos.Comment;
using marketplace_api.Interfaces;
using marketplace_api.Mappers;
using marketplace_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace marketplace_api.Controllers;

[Route("api/v1/comments")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    private readonly IStockRepository _stockRepository;

    public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
    {
        _commentRepository = commentRepository;
        _stockRepository = stockRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var comments = await _commentRepository.GetAllAsync();

        var commentDto = comments.Select(s => s.ToCommentDto());
        
        return Ok(commentDto);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var comment = await _commentRepository.GetByIdAsync(id);

        if (comment is null)
        {
            return NotFound();
        }
        
        return Ok(comment.ToCommentDto());
    }

    [HttpPost]
    [Route("{stockId}")]
    [Authorize]
    public async Task<IActionResult> Create([FromRoute] Guid stockId, CreateCommentRequestDto commentDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        if (!await _stockRepository.StockExists(stockId))
        {
            return BadRequest("Stock has not been found");
        }
        
        var commentModel = commentDto.ToCommentFromCreate(stockId);

        await _commentRepository.CreateAsync(commentModel);
        return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
    }

    [HttpPut]
    [Route("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateCommentRequestDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var comment = await _commentRepository.UpdateAsync(id, updateDto.ToCommentFromUpdate());

        if (comment is null)
        {
            return NotFound("Comment not found");
        }
        
        return Ok(comment.ToCommentDto());
    }

    [HttpDelete]
    [Route("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var commentModel = await _commentRepository.DeleteAsync(id);

        if (commentModel is null)
        {
            return NotFound("Comment not found");
        }
        
        return Ok();
    }
}