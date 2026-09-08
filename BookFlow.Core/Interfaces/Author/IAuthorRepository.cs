using BookFlow.Core.Enums;
using BookFlow.Core.DTOs;
namespace BookFlow.Core.Interfaces;

public interface IAuthorRepository
{
    Task<IEnumerable<AuthorResponseDto>> GetAllAsync();

    Task<int> CreateAsync(AuthorCreateDto author);

    Task<bool> UpdateAsync(AuthorUpdateDto author, int id);

    Task<DeleteResult> DeleteAsync(int id);

    Task<bool> IsExistsAsync(int id);
}
