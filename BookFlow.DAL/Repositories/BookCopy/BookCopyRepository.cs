using BookFlow.Core.Entities;
using BookFlow.Core.Interfaces;
using BookFlow.DAL.Context;
using Microsoft.EntityFrameworkCore;

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

    public async Task<List<int>> GetCopyNumbersAsync(int bookId)
    {
        return await _context.BookCopies
            .Where(x => x.BookId == bookId)
            .Select(x => x.CopyNumber)
            .ToListAsync();
    }
   
}
