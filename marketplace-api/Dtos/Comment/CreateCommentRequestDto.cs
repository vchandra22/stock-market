namespace marketplace_api.Dtos.Comment;

public class CreateCommentRequestDto
{
    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;
}