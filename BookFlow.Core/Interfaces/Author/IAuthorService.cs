using BookFlow.Core.DTOs;
namespace BookFlow.Core.Interfaces;

public interface IAuthorService
{
    Task<IEnumerable<AuthorResponseDto>> GetAllAsync();

    Task<int> CreateAsync(AuthorCreateDto author);

    Task<bool> UpdateAsync(AuthorUpdateDto author, int id);

    Task<bool> DeleteAsync(int id);
}
