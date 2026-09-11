using BookFlow.Core.DTOs;
using BookFlow.Core.Entities;
using BookFlow.Core.Enums;
using BookFlow.Core.Interfaces;
using BookFlow.DAL.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookFlow.DAL.Repositories;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BookResponseDto>> GetAllAsync()
    {
        return await _context.Books
            .AsNoTracking()
            .Select(BookResponseProjection)
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    public async Task<BookResponseDto?> GetByIdAsync(int id)
    {
        return await _context.Books
            .AsNoTracking()
            .Select(BookResponseProjection)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<Book?> GetByIdForUpdateAsync(int id)
    {
        return await _context.Books
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> CreateAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book.Id;
    }

    public async Task<bool> UpdateAsync(Book book)
    {
        _context.Books.Update(book);
        var affectedRows = await _context.SaveChangesAsync();
        return affectedRows > 0;
    }

    public async Task<DeleteResult> DeleteAsync(int id)
    {
        try
        {
            _context.Books.Remove(new Book { Id = id });
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
        return await _context.Books
            .AnyAsync(x => x.Id == id);
    }

    public async Task<bool> IsExistByIsbnAsync(string isbn)
    {
        return await _context.Books
            .AnyAsync(x => x.ISBN == isbn);
    }
    private static readonly Expression<Func<Book, BookResponseDto>> BookResponseProjection =
    x => new BookResponseDto
    {
        Id = x.Id,
        Title = x.Title,
        ISBN = x.ISBN,
        Author = x.Author,
        Category = x.Category,
        PublishedDate = x.PublishedDate,
        CreatedAt = x.CreatedAt,
        TotalCopies = x.Copies.Count(),
        AvailableCopies = x.Copies.Count(c =>
            c.Status == BookCopyStatus.Available),
        BorrowedCopies = x.Copies.Count(c =>
            c.Status == BookCopyStatus.Borrowed),
        DamagedCopies = x.Copies.Count(c =>
            c.Status == BookCopyStatus.Damaged),
        LostCopies = x.Copies.Count(c =>
            c.Status == BookCopyStatus.Lost)
    };

}
