using BookFlow.Core.Interfaces;
using Microsoft.Data.SqlClient;
using BookFlow.Core.Entities;
using BookFlow.DAL.Context;
using Microsoft.EntityFrameworkCore;
using BookFlow.Core.Enums;
using BookFlow.Core.DTOs;

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
            .Select(x => new AuthorResponseDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                CountryName = x.Country.Name,
                CreatedAt = x.CreatedAt
            })
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    public async Task<int> CreateAsync(AuthorCreateDto author)
    {
        var newAuthor = new Author
        {
            FirstName = author.FirstName,
            LastName = author.LastName,
            CountryId = author.CountryId,
            CreatedAt = DateTime.UtcNow
        };
        _context.Authors.Add(newAuthor);
        await _context.SaveChangesAsync();
        return newAuthor.Id;
    }

    public async Task<bool> UpdateAsync(AuthorUpdateDto author, int id)
    {
        var existingAuthor = await _context.Authors.FindAsync(id);
        if (existingAuthor == null)
            return false;

        existingAuthor.FirstName = author.FirstName;
        existingAuthor.LastName = author.LastName;
        existingAuthor.CountryId = author.CountryId;

        _context.Authors.Update(existingAuthor);
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

}
