using BookFlow.Core.DTOs;
using BookFlow.Core.Entities;

namespace BookFlow.Core.Interfaces;

public interface IBookCopyRepository
{
    Task AddRangeAsync(IEnumerable<BookCopy> copies);

    Task<IEnumerable<BookCopyResponseDto>> GetCopyNumbersAsync(int bookId);

}
