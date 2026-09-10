using BookFlow.Core.Interfaces;
using Microsoft.Data.SqlClient;
using BookFlow.Core.Entities;
using BookFlow.DAL.Context;
using Microsoft.EntityFrameworkCore;
using BookFlow.Core.Enums;
using BookFlow.Core.DTOs;
using System.Linq.Expressions;

namespace BookFlow.DAL.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly AppDbContext _context;

    public MemberRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MemberResponseDto>> GetAllAsync()
    {
        return await _context.Members
            .AsNoTracking()
            .Select(MemberResponseProjection)
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    public async Task<MemberResponseDto?> GetByIdAsync(int id)
    {
        var author = await _context.Members
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(MemberResponseProjection)
            .FirstOrDefaultAsync();
        return author;
    }

    public async Task<Member?> GetByIdForUpdateAsync(int id)
    {
        var author = await _context.Members
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
        return author;
    }

    public async Task<int> CreateAsync(Member author)
    {
        _context.Members.Add(author);
        await _context.SaveChangesAsync();
        return author.Id;
    }

    public async Task<bool> UpdateAsync(Member author)
    {
        _context.Members.Update(author);
        var affectedRows = await _context.SaveChangesAsync();
        return affectedRows > 0;
    }

    public async Task<DeleteResult> DeleteAsync(int id)
    {
        try
        {
            _context.Members.Remove(new Member { Id = id });
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
        return await _context.Members
            .AnyAsync(x => x.Id == id);
    }

    public async Task<bool> IsActiveAsync(int id)
    {
        return await _context.Members
            .AnyAsync(x => x.Id == id && x.IsActive);
    }

    private static readonly Expression<Func<Member, MemberResponseDto>> MemberResponseProjection =
    x => new MemberResponseDto
    {
        Id = x.Id,
        FirstName = x.FirstName,
        LastName = x.LastName,
        Phone = x.Phone,
        IsActive = x.IsActive,
        CreatedAt = x.CreatedAt
    };

}
