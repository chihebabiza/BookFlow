namespace BookFlow.Core.DTOs;

public class BookCopyCountsDto
{
    public int Available { get; set; }
    public int Total { get; set; }
    public int Borrowed { get; set; }
    public int Damaged { get; set; }
    public int Lost { get; set; }
}