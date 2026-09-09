using BookFlow.Core.DTOs;
namespace BookFlow.Core.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryResponseDto>> GetAllAsync();
    Task<CategoryResponseDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(CategoryCreateDto dto);
    Task<bool> UpdateAsync(CategoryUpdateDto dto, int id);
    Task<bool> DeleteAsync(int id);
}
