using BookFlow.Core.DTOs;
using BookFlow.Core.Entities;
using BookFlow.Core.Enums;

namespace BookFlow.Core.Interfaces;

public interface IBookRepository
{
    Task<IEnumerable<BookResponseDto>> GetAllAsync();

    Task<BookResponseDto?> GetByIdAsync(int id);

    Task<Book?> GetByIdForUpdateAsync(int id);

    Task<int> CreateAsync(Book book);

    Task<bool> UpdateAsync(Book book);

    Task<DeleteResult> DeleteAsync(int id);

    Task<bool> IsExistsAsync(int id);

    Task<bool> IsExistByIsbnAsync(string isbn);
}
