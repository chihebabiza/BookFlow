using BookFlow.Core.Entities;
namespace BookFlow.Core.DTOs;

public class BookResponseDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string ISBN { get; set; } = string.Empty;

    public DateTime? PublishedDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Author Author { get; set; } = null!;

    public Category Category{ get; set; } = null!;

    public int AvailableCopies { get; set; }
    public int TotalCopies { get; set; }
    public int BorrowedCopies { get; set; }
    public int DamagedCopies { get; set; }
    public int LostCopies { get; set; }
}
