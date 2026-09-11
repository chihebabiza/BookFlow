namespace BookFlow.Core.DTOs;

public class BookCopyResponseDto
{
    public int Id { get; set; }
    public int CopyNumber { get; set; }

    public BookResponseDto Book { set; get; } = null!;
}
