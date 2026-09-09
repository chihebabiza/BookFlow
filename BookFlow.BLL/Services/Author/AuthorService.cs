using BookFlow.BLL.Exceptions;
using BookFlow.BLL.Helpers;
using BookFlow.Core.DTOs;
using BookFlow.Core.Entities;
using BookFlow.Core.Enums;
using BookFlow.Core.Interfaces;

namespace BookFlow.BLL.Services;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepository _repository;
    private readonly ICountryRepository _countryRepository;

    public AuthorService(
        IAuthorRepository repository,
        ICountryRepository countryRepository)
    {
        _repository = repository;
        _countryRepository = countryRepository;
    }

    public async Task<IEnumerable<AuthorResponseDto>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<AuthorResponseDto> GetByIdAsync(int id)
    {
        ValidationHelper.ValidateId(id);

        var author = await _repository.GetByIdAsync(id);

        if (author is null)
            throw new NotFoundException(
                $"The author with the identifier {id} does not exist");

        return author;
    }

    public async Task<int> CreateAsync(AuthorCreateDto dto)
    {
        var countryExists = await _countryRepository.IsExistsAsync(dto.CountryId);

        if (!countryExists)
            throw new NotFoundException(
                $"The country with the identifier {dto.CountryId} does not exist");

        var author = new Author
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            CountryId = dto.CountryId,
            CreatedAt = DateTime.UtcNow
        };

        return await _repository.CreateAsync(author);
    }

    public async Task<bool> UpdateAsync(AuthorUpdateDto dto, int id)
    {
        ValidationHelper.ValidateId(id);

        // Check if author exists
        var author = await _repository.GetByIdForUpdateAsync(id);

        if (author is null)
            throw new NotFoundException(
                $"The author with the identifier {id} does not exist");

        // Check if country exists
        var countryExists = await _countryRepository.IsExistsAsync(dto.CountryId);

        if (!countryExists)
            throw new NotFoundException(
                $"The country with the identifier {dto.CountryId} does not exist");

        // Update entity
        author.FirstName = dto.FirstName;
        author.LastName = dto.LastName;
        author.CountryId = dto.CountryId;

        return await _repository.UpdateAsync(author);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        ValidationHelper.ValidateId(id);

        var exists = await _repository.IsExistsAsync(id);

        if (!exists)
            throw new NotFoundException(
                $"The author with the identifier {id} does not exist");

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
