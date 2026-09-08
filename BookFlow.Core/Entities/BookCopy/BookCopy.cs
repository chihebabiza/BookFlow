using BookFlow.Core.Enums;
namespace BookFlow.Core.Entities;

public class BookCopy
{
    public int Id { get; set; }

    public int BookId { get; set; }

    public Book Book { get; set; } = null!;

    public string CopyNumber { get; set; } = string.Empty;

    public BookCopyStatus Status { get; set; } = BookCopyStatus.Available;

    public DateTime AcquiredDate { get; set; }

    //public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
