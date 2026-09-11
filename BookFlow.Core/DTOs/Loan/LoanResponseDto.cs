using BookFlow.Core.Enums;
using BookFlow.Core.Entities;
namespace BookFlow.Core.DTOs;

public class LoanResponseDto
{
    public int Id { get; set; }

    public Member Member { get; set; } = null!;

    public BookCopy BookCopy { get; set; } = null!;

    public Book Book { get; set; } = null!;

    public DateTime BorrowedDate { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? ReturnedDate { get; set; }

    public LoanStatus Status { get; set; }
}
