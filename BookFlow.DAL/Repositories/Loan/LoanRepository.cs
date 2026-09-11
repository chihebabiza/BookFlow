using BookFlow.Core.Interfaces;
using Microsoft.Data.SqlClient;
using BookFlow.Core.Entities;
using BookFlow.DAL.Context;
using Microsoft.EntityFrameworkCore;
using BookFlow.Core.Enums;
using BookFlow.Core.DTOs;
using System.Linq.Expressions;

namespace BookFlow.DAL.Repositories;

public class LoanRepository : ILoanRepository
{
    private readonly AppDbContext _context;

    public LoanRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LoanResponseDto>> GetByMemberAsync(int memberId)
    {
        return await _context.Loans
            .AsNoTracking()
            .Where(x=>x.MemberId == memberId)
            .Select(LoanResponseProjection)
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    public async Task<LoanResponseDto?> GetByIdAsync(int id)
    {
        var author = await _context.Loans
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(LoanResponseProjection)
            .FirstOrDefaultAsync();
        return author;
    }

    public async Task<Loan?> GetByIdForUpdateAsync(int id)
    {
        var author = await _context.Loans
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
        return author;
    }

    public void Create(Loan author)
    {
        _context.Loans.Add(author);
    }

    public void Update(Loan author)
    {
        _context.Loans.Update(author);
    }

    public async Task<DeleteResult> DeleteAsync(int id)
    {
        try
        {
            _context.Loans.Remove(new Loan { Id = id });
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
        return await _context.Loans
            .AnyAsync(x => x.Id == id);
    }

    private static readonly Expression<Func<Loan, LoanResponseDto>> LoanResponseProjection =
    x => new LoanResponseDto
    {
        Id = x.Id,
        BookCopy = new BookCopyResponseDto
        {
            Id = x.BookCopy.Id,
            CopyNumber = x.BookCopy.CopyNumber,
        },
        Member = new MemberResponseDto
        {
            Id = x.Member.Id,
            FirstName = x.Member.FirstName,
            LastName = x.Member.LastName,
            Phone = x.Member.Phone,
            CreatedAt = x.Member.CreatedAt,
            IsActive = x.Member.IsActive
        },
        BorrowedDate = x.BorrowedDate,
        DueDate = x.DueDate,
        ReturnedDate = x.ReturnedDate,
        Status = x.Status,
    };

}
