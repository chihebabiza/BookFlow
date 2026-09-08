using BookFlow.Core.Interfaces;
using BookFlow.Core.DTOs;
using BookFlow.BLL.Exceptions;

namespace BookFlow.BLL.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(
        ICategoryRepository repository)
    {
        _repository = repository;
    }

}
