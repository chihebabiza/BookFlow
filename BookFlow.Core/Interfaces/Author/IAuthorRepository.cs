using BookFlow.Core.Enums;
using BookFlow.Core.DTOs;
using BookFlow.Core.Entities;
namespace BookFlow.Core.Interfaces;

public interface IAuthorRepository
{
    Task<IEnumerable<AuthorResponseDto>> GetAllAsync();

    Task<AuthorResponseDto?> GetByIdAsync(int id);

    Task<Author?> GetByIdForUpdateAsync(int id);

    Task<int> CreateAsync(Author author);

    Task<bool> UpdateAsync(Author author);

    Task<DeleteResult> DeleteAsync(int id);

    Task<bool> IsExistsAsync(int id);
}
