namespace BookFlow.Core.DTOs;

public class BookUpdateDto
{
    public string Title { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public DateTime? PublishedDate { get; set; }
    public int AuthorId { get; set; }
    public int CategoryId { get; set; }
}
