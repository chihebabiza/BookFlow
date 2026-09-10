using BookFlow.Core.Enums;
namespace BookFlow.Core.Entities;

public class BookCopy
{
    public int Id { get; set; }

    public string Barcode { get; set; } = string.Empty;

    public BookCopyStatus Status { get; set; } = BookCopyStatus.Available;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Relationship
    public int BookId { get; set; }

    public Book Book { get; set; } = null!;

    // Navigation property for loans
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
