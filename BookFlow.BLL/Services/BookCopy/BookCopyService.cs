using BookFlow.Core.Interfaces;
using BookFlow.BLL.Exceptions;

namespace BookFlow.BLL.Services;

public class BookCopyService : IBookCopyService
{
    private readonly IBookCopyRepository _repository;
    private readonly IBookRepository _bookRepository;

    public BookCopyService(
        IBookCopyRepository repository,
        IBookRepository bookRepository)
    {
        _repository = repository;
        _bookRepository = bookRepository;
    }

    public async Task<List<int>> GetCopyNumbersAsync(int bookId)
    {
        if(bookId <= 0)
            throw new ArgumentException("The book identifier must be greater than zero", nameof(bookId));

        var bookExists = await _bookRepository.IsExistsAsync(bookId);
        if (!bookExists)
            throw new NotFoundException(
                $"The book with the identifier {bookId} does not exist");

        return await _repository.GetCopyNumbersAsync(bookId);
    }

}
