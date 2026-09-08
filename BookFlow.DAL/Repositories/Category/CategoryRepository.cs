using Microsoft.Data.SqlClient;
using BookFlow.Core.Interfaces;
using BookFlow.Core.Entities;
using BookFlow.DAL.Context;
using BookFlow.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace BookFlow.DAL.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _context.Categories
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    public async Task<int> CreateAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        return category.Id;
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

}
