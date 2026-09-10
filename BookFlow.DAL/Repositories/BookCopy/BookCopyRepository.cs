using BookFlow.Core.Entities;
using BookFlow.Core.Interfaces;
using BookFlow.DAL.Context;

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
   
}
