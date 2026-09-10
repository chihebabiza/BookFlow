namespace BookFlow.Core.DTOs;

public class LoanCreateDto
{
    public int MemberId { get; set; }

    public int BookCopyId { get; set; }

    public DateTime BorrowedDate { get; set; }

    public int Period { get; set; }

}
