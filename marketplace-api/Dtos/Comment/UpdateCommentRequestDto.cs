using System.ComponentModel.DataAnnotations;

namespace marketplace_api.Dtos.Comment;

public class UpdateCommentRequestDto
{
    [Required]
    [MinLength(5, ErrorMessage = "Name must be at least 5 characters long.")]
    [MaxLength(50, ErrorMessage = "Name must be no more than 50 characters long.")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MinLength(5, ErrorMessage = "Content must be at least 5 characters long.")]
    [MaxLength(500, ErrorMessage = "Content must be no more than 500 characters long.")]
    public string Content { get; set; } = string.Empty;
}