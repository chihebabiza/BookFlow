using BookFlow.Core.Entities;

namespace BookFlow.Core.Interfaces;

public interface IBookCopyRepository
{
    Task AddRangeAsync(IEnumerable<BookCopy> copies);

}
