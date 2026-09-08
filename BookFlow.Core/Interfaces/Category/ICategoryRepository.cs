using BookFlow.Core.Entities;
using BookFlow.Core.Enums;

namespace BookFlow.Core.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();

    Task<int> CreateAsync(Category category);

    Task<DeleteResult> DeleteAsync(int id);

    Task<bool> IsExistsAsync(int id);

    Task<bool> IsExistByNameAsync(string name);
}
