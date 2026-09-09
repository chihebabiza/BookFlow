namespace BookFlow.Core.DTOs;

public class BookResponseDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string ISBN { get; set; } = string.Empty;

    public DateTime? PublishedDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string AuthorName { get; set; } = null!;

    public string CategoryName { get; set; } = null!;
}
