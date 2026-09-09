using BookFlow.Core.DTOs;
using BookFlow.Core.Entities;
using BookFlow.Core.Enums;
using BookFlow.Core.Interfaces;
using BookFlow.DAL.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookFlow.DAL.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetAllAsync()
    {
        return await _context.Categories
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .Select(CategoryResponseProjection)
            .ToListAsync();
    }

    public async Task<CategoryResponseDto?> GetByIdAsync(int id)
    {
        return await _context.Categories
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(CategoryResponseProjection)
            .FirstOrDefaultAsync();
    }

    public async Task<Category?> GetByIdForUpdateAsync(int id)
    {
        return await _context.Categories
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<int> CreateAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return category.Id;
    }

    public async Task<bool> UpdateAsync(Category category)
    {
        _context.Categories.Update(category);
        var affectedRows = await _context.SaveChangesAsync();
        return affectedRows > 0;
    }

    public async Task<DeleteResult> DeleteAsync(int id)
    {
        try
        {
            _context.Categories.Remove(new Category { Id = id });
            await _context.SaveChangesAsync();
            return DeleteResult.Success;
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
        {
            return DeleteResult.HasDependencies;
        }
        catch (Exception)
        {
            return DeleteResult.SqlProblem;
        }
    }

    public async Task<bool> IsExistsAsync(int id)
    {
        return await _context.Categories
            .AnyAsync(x => x.Id == id);
    }

    public async Task<bool> IsExistByNameAsync(string name)
    {
        return await _context.Categories
            .AnyAsync(x => x.Name == name);
    }

    private static readonly Expression<Func<Category, CategoryResponseDto>> CategoryResponseProjection =
    x => new CategoryResponseDto
    {
        Id = x.Id,
        Name = x.Name,
        CreatedAt = x.CreatedAt
    };

}
