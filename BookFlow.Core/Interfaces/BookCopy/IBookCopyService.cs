using BookFlow.Core.DTOs;
namespace BookFlow.Core.Interfaces;

public interface IBookCopyService
{
    Task<IEnumerable<BookCopyResponseDto>> GetCopyNumbersAsync(int bookId);
}
