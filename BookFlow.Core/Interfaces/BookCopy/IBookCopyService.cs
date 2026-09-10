using BookFlow.Core.DTOs;
namespace BookFlow.Core.Interfaces;

public interface IBookCopyService
{
    Task<List<int>> GetCopyNumbersAsync(int bookId);
}
