using BookFlow.Core.DTOs;
using BookFlow.Core.Entities;

namespace BookFlow.Core.Interfaces;

public interface IBookCopyRepository
{
    Task<IEnumerable<BookCopyResponseDto>> GetAvailableAsync(int bookId);

    Task<BookCopy?> GetByIdForUpdateAsync(int id);

    Task AddRangeAsync(IEnumerable<BookCopy> copies);

    void Update(BookCopy bookCopy);

    Task<bool> IsExistsAsync(int id);
}
