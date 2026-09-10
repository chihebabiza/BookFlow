using BookFlow.Core.DTOs;
using BookFlow.Core.Entities;

namespace BookFlow.Core.Interfaces;

public interface IBookCopyRepository
{
    Task AddRangeAsync(IEnumerable<BookCopy> copies);

    Task<IEnumerable<BookCopyResponseDto>> GetAvailableAsync(int bookId);

    Task<bool> IsExistsAsync(int id);

    Task<BookCopy?> GetByIdForUpdateAsync(int id);

    Task<bool> UpdateAsync(BookCopy bookCopy);

}
