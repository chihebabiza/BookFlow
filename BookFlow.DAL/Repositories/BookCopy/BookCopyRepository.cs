using BookFlow.Core.DTOs;
using BookFlow.Core.Entities;
using BookFlow.Core.Interfaces;
using BookFlow.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookFlow.DAL.Repositories;

public class BookCopyRepository : IBookCopyRepository
{
    private readonly AppDbContext _context;

    public BookCopyRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddRangeAsync(IEnumerable<BookCopy> copies)
    {
        await _context.BookCopies.AddRangeAsync(copies);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<BookCopyResponseDto>> GetCopyNumbersAsync(int bookId)
    {
        return await _context.BookCopies
            .Where(x => x.BookId == bookId)
            .Select(BookCopyResponseProjection)
            .ToListAsync();
    }

    public async Task<bool> IsExistsAsync(int id)
    {
        return await _context.BookCopies
            .AnyAsync(x => x.Id == id);
    }

    private static readonly Expression<Func<BookCopy, BookCopyResponseDto>> BookCopyResponseProjection =
    x => new BookCopyResponseDto
    {
       Id = x.Id,
       CopyNumber = x.CopyNumber
    };


}
