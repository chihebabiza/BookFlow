using BookFlow.Core.Interfaces;
using BookFlow.Core.DTOs;
using BookFlow.BLL.Exceptions;

namespace BookFlow.BLL.Services;

public class BookCopyService : IBookCopyService
{
    private readonly IBookCopyRepository _repository;

    public BookCopyService(
        IBookCopyRepository repository)
    {
        _repository = repository;
    }

}
