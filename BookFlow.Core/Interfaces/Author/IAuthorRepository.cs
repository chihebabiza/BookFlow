using BookFlow.Core.Entities;
using BookFlow.Core.Enums;

namespace BookFlow.Core.Interfaces;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAllAsync();

    Task<int> CreateAsync(Author author);

    Task<bool> IsExistsAsync(int id);

    Task<bool> UpdateAsync(Author author);

    Task<DeleteResult> DeleteAsync(int id);
}
