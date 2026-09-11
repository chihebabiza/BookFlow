using BookFlow.Core.Enums;

namespace BookFlow.Core.DTOs;

public class LoanResponseDto
{
    public int Id { get; set; }

    public MemberResponseDto Member { get; set; } = null!;

    public BookCopyResponseDto BookCopy { get; set; } = null!;

    public DateTime BorrowedDate { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? ReturnedDate { get; set; }

    public LoanStatus Status { get; set; }
}
