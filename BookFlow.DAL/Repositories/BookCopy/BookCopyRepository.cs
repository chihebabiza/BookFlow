using BookFlow.Core.DTOs;
using BookFlow.Core.Entities;
using BookFlow.Core.Enums;
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

    public async Task<IEnumerable<BookCopyResponseDto>> GetAvailableAsync(int bookId)
    {
        return await _context.BookCopies
            .Where(x => x.BookId == bookId && x.Status == BookCopyStatus.Available)
            .Select(BookCopyResponseProjection)
            .ToListAsync();
    }

    public async Task<bool> IsExistsAsync(int id)
    {
        return await _context.BookCopies
            .AnyAsync(x => x.Id == id);
    }

    public async Task<BookCopy?> GetByIdForUpdateAsync(int id)
    {
        return await _context.BookCopies
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateAsync(BookCopy bookCopy)
    {
        _context.BookCopies.Update(bookCopy);
        var result = await _context.SaveChangesAsync();
        return result > 0;
    }

    private static readonly Expression<Func<BookCopy, BookCopyResponseDto>> BookCopyResponseProjection =
    x => new BookCopyResponseDto
    {
       Id = x.Id,
       CopyNumber = x.CopyNumber
    };


}
