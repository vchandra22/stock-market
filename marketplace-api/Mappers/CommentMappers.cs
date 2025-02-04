using marketplace_api.Dtos.Comment;
using marketplace_api.Models;

namespace marketplace_api.Mappers;

public static class CommentMappers
{
    public static CommentDto ToCommentDto(this Comment commentModel)
    {
        return new CommentDto
        {
            Id = commentModel.Id,
            Title = commentModel.Title,
            Content = commentModel.Content,
            CreatedOn = commentModel.CreatedOn,
            CreatedBy = commentModel.AppUser.UserName,
            StockId = commentModel.StockId,
        };
    }
    
    public static Comment ToCommentFromCreate(this CreateCommentRequestDto commentDto, Guid stockId)
    {
        return new Comment
        {
            Title = commentDto.Title,
            Content = commentDto.Content,
            StockId = stockId,
        };
    }
    
    public static Comment ToCommentFromUpdate(this UpdateCommentRequestDto commentDto)
    {
        return new Comment
        {
            Title = commentDto.Title,
            Content = commentDto.Content
        };
    }
}