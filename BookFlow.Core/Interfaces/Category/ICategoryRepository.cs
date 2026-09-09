using BookFlow.Core.Entities;
using BookFlow.Core.Enums;
using BookFlow.Core.DTOs;

namespace BookFlow.Core.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<CategoryResponseDto>> GetAllAsync();

    Task<int> CreateAsync(Category category);

    Task<DeleteResult> DeleteAsync(int id);

    Task<bool> IsExistsAsync(int id);

    Task<bool> IsExistByNameAsync(string name);
}
