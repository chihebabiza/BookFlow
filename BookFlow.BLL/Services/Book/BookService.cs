using BookFlow.Core.Interfaces;
using BookFlow.Core.DTOs;
using BookFlow.BLL.Exceptions;

namespace BookFlow.BLL.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;

    public BookService(
        IBookRepository repository)
    {
        _repository = repository;
    }

}
