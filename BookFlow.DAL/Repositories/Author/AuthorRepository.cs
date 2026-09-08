using BookFlow.Core.Interfaces;
using Microsoft.Data.SqlClient;
using BookFlow.Core.Entities;
using BookFlow.DAL.Context;
using Microsoft.EntityFrameworkCore;
using BookFlow.Core.Enums;

namespace BookFlow.DAL.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly AppDbContext _context;

    public AuthorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Author>> GetAllAsync()
    {
        return await _context.Authors
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    public async Task<int> CreateAsync(Author author)
    {
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
        return author.Id;
    }

    public async Task<bool> IsExistsAsync(int id)
    {
        return await _context.Authors
            .AnyAsync(x => x.Id == id);
    }

    public async Task<bool> UpdateAsync(Author author)
    {
        _context.Authors.Update(author);
        var affectedRows = await _context.SaveChangesAsync();
        return affectedRows > 0;
    }

    public async Task<DeleteResult> DeleteAsync(int id)
    {
        try
        {
            _context.Authors.Remove(new Author { Id = id });
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

}
