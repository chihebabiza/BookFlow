using BookFlow.Core.Interfaces;
using Microsoft.Data.SqlClient;
using BookFlow.Core.Entities;
using BookFlow.DAL.Context;
using Microsoft.EntityFrameworkCore;
using BookFlow.Core.Enums;
using BookFlow.Core.DTOs;
using System.Linq.Expressions;

namespace BookFlow.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserResponseDto>> GetAllAsync()
    {
        return await _context.Users
            .AsNoTracking()
            .Select(UserResponseProjection)
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    public async Task<UserResponseDto?> GetByIdAsync(int id)
    {
        var author = await _context.Users
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(UserResponseProjection)
            .FirstOrDefaultAsync();
        return author;
    }

    public async Task<User?> GetByIdForUpdateAsync(int id)
    {
        var author = await _context.Users
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
        return author;
    }

    public async Task<int> CreateAsync(User author)
    {
        _context.Users.Add(author);
        await _context.SaveChangesAsync();
        return author.Id;
    }

    public async Task<bool> UpdateAsync(User author)
    {
        _context.Users.Update(author);
        var affectedRows = await _context.SaveChangesAsync();
        return affectedRows > 0;
    }

    public async Task<DeleteResult> DeleteAsync(int id)
    {
        try
        {
            _context.Users.Remove(new User { Id = id });
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
        return await _context.Users
            .AnyAsync(x => x.Id == id);
    }

    private static readonly Expression<Func<User, UserResponseDto>> UserResponseProjection =
    x => new UserResponseDto
    {
        Id = x.Id,
        //CreatedAt = x.CreatedAt
    };

}
