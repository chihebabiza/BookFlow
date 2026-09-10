namespace BookFlow.Core.DTOs;

public class BookCreateDto
{
    public string Title { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public int AuthorId { get; set; }
    public int CategoryId { get; set; }
    public DateTime PublishedDate { get; set; }
    public int Quantity { get; set; }
}
