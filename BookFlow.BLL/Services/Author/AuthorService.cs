using BookFlow.BLL.Exceptions;
using BookFlow.Core.DTOs;
using BookFlow.Core.Enums;
using BookFlow.Core.Interfaces;

namespace BookFlow.BLL.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _repository;

    public AuthorService(
        IAuthorRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AuthorResponseDto>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<int> CreateAsync(AuthorCreateDto author)
    {
        return await _repository.CreateAsync(author);
    }

    public async Task<bool> UpdateAsync(AuthorUpdateDto author, int id)
    {
        var exist = await _repository.IsExistsAsync(id);
        if (!exist)
            throw new NotFoundException($"The author with the identifier {id} does not exist");

        return await _repository.UpdateAsync(author, id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var exist = await _repository.IsExistsAsync(id);
        if (!exist)
            throw new NotFoundException($"The author with the identifier {id} does not exist");

        var result = await _repository.DeleteAsync(id);
        return result switch
        {
            DeleteResult.Success => true,

            DeleteResult.HasDependencies => throw new ConflictException(
                "This author cannot be deleted because it has dependencies"),

            DeleteResult.SqlProblem => throw new Exception(
                "An error occurred while deleting the author"),

            _ => throw new Exception(
                "An unexpected error occurred while deleting the author")
        };
    }
}
