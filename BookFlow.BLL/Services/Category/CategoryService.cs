using BookFlow.BLL.Exceptions;
using BookFlow.BLL.Helpers;
using BookFlow.Core.DTOs;
using BookFlow.Core.Entities;
using BookFlow.Core.Enums;
using BookFlow.Core.Interfaces;

namespace BookFlow.BLL.Services;
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(
        ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<CategoryResponseDto?> GetByIdAsync(int id)
    {
        ValidationHelper.ValidateId(id);

        var category = await _repository.GetByIdAsync(id);
        if (category == null)
            throw new NotFoundException($"Category with ID {id} not found.");

        return category;
    }

    public async Task<int> CreateAsync(CategoryCreateDto dto)
    {
        var existingCategory = await _repository.IsExistByNameAsync(dto.Name);
        if (!existingCategory)
            throw new ConflictException($"Category with name '{dto.Name}' already exists.");

        var category = new Category
        {
            Name = dto.Name
        };
        return await _repository.CreateAsync(category);
    }

    public async Task<bool> UpdateAsync(CategoryUpdateDto dto, int id)
    {
        ValidationHelper.ValidateId(id);

        var category = await _repository.GetByIdForUpdateAsync(id);

        if (category is null)
            throw new NotFoundException($"The category with the identifier {id} does not exist");

        var existingCategory = await _repository.IsExistByNameAsync(dto.Name);

        if (!existingCategory)
            throw new ConflictException($"Category with name '{dto.Name}' already exists.");

        category.Name = dto.Name;

        return await _repository.UpdateAsync(category);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        ValidationHelper.ValidateId(id);

        var exists = await _repository.IsExistsAsync(id);

        if (!exists)
            throw new NotFoundException(
                $"The category with the identifier {id} does not exist");

        var result = await _repository.DeleteAsync(id);

        return result switch
        {
            DeleteResult.Success => true,

            DeleteResult.HasDependencies => throw new ConflictException(
                "This category cannot be deleted because it has dependencies"),

            DeleteResult.SqlProblem => throw new Exception(
                "An error occurred while deleting the category"),

            _ => throw new Exception(
                "An unexpected error occurred while deleting the category")
        };
    }

}
