using BookFlow.Core.DTOs;
using BookFlow.Core.Entities;
using BookFlow.Core.Enums;
using BookFlow.Core.Interfaces;
using BookFlow.DAL.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookFlow.DAL.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly AppDbContext _context;

    public AuthorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AuthorResponseDto>> GetAllAsync()
    {
        return await _context.Authors
            .AsNoTracking()
            .Select(AuthorResponseProjection)
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    public async Task<AuthorResponseDto?> GetByIdAsync(int id)
    {
        var author = await _context.Authors
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(AuthorResponseProjection)
            .FirstOrDefaultAsync();
        return author;
    }

    public async Task<Author?> GetByIdForUpdateAsync(int id)
    {
        var author = await _context.Authors
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
        return author;
    }

    public async Task<int> CreateAsync(Author author)
    {
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
        return author.Id;
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

    public async Task<bool> IsExistsAsync(int id)
    {
        return await _context.Authors
            .AnyAsync(x => x.Id == id);
    }

    private static readonly Expression<Func<Author, AuthorResponseDto>> AuthorResponseProjection =
    x => new AuthorResponseDto
    {
        Id = x.Id,
        FirstName = x.FirstName,
        LastName = x.LastName,
        Country = new CountryResponseDto
        {
            Id = x.Country.Id,
            Code = x.Country.Code,
            Name = x.Country.Name
        },
        CreatedAt = x.CreatedAt
    };

}
