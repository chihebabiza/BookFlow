using BookFlow.Core.Entities;
using BookFlow.Core.Enums;

namespace BookFlow.Core.Interfaces;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllAsync();

    Task<Book?> GetByIdAsync(int id);

    Task<int> CreateAsync(Book book);

    Task<Book?> GetByIdForUpdateAsync(int id);

    Task<bool> UpdateAsync(Book book);

    Task<DeleteResult> DeleteAsync(int id);
}
