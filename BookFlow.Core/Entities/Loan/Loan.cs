using BookFlow.Core.Enums;
namespace BookFlow.Core.Entities;

public class Loan
{
    public int Id { get; set; }

    public int MemberId { get; set; }

    public int BookCopyId { get; set; }

    public DateTime BorrowedDate { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? ReturnedDate { get; set; }

    public LoanStatus Status { get; set; } = LoanStatus.Active;

    // Navigation properties
    public Member Member { get; set; } = null!;

    public BookCopy BookCopy { get; set; } = null!;
}
