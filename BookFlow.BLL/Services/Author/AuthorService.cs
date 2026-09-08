using BookFlow.Core.Interfaces;
using BookFlow.Core.DTOs;
using BookFlow.BLL.Exceptions;

namespace BookFlow.BLL.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _repository;

    public AuthorService(
        IAuthorRepository repository)
    {
        _repository = repository;
    }

}
