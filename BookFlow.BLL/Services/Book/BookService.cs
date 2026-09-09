using BookFlow.BLL.Exceptions;
using BookFlow.Core.DTOs;
using BookFlow.Core.Entities;
using BookFlow.Core.Enums;
using BookFlow.Core.Interfaces;

namespace BookFlow.BLL.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;
    private readonly IAuthorRepository _authorRepository;
    private readonly ICategoryRepository _categoryRepository;

    public BookService(
        IBookRepository repository,
        IAuthorRepository authorRepository,
        ICategoryRepository categoryRepository)
    {
        _repository = repository;
        _authorRepository = authorRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<BookResponseDto>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<BookResponseDto> GetByIdAsync(int id)
    {
        ValidateId(id);

        var book = await _repository.GetByIdAsync(id);

        if (book is null)
            throw new NotFoundException(
                $"The book with the identifier {id} does not exist");

        return book;
    }

    public async Task<int> CreateAsync(BookCreateDto dto)
    {
        var authorExists = await _authorRepository.IsExistsAsync(dto.AuthorId);

        if (!authorExists)
            throw new NotFoundException(
                $"The author with the identifier {dto.AuthorId} does not exist");

        var categoryExists = await _categoryRepository.IsExistsAsync(dto.CategoryId);

        if (!categoryExists)
            throw new NotFoundException(
                $"The category with the identifier {dto.CategoryId} does not exist");

        var isbnExists = await _repository.IsExistByIsbnAsync(dto.ISBN);

        if (isbnExists)
            throw new ConflictException(
                $"A book with the ISBN {dto.ISBN} already exists");

        var book = new Book
        {
            Title = dto.Title,
            ISBN = dto.ISBN,
            AuthorId = dto.AuthorId,
            CategoryId = dto.CategoryId,
            PublishedDate = dto.PublishedDate,
            CreatedAt = DateTime.UtcNow
        };

        return await _repository.CreateAsync(book);
    }

    public async Task<bool> UpdateAsync(BookUpdateDto dto, int id)
    {
        ValidateId(id);

        var book = await _repository.GetByIdForUpdateAsync(id);

        if (book is null)
            throw new NotFoundException(
                $"The book with the identifier {id} does not exist");

        var authorExists = await _authorRepository.IsExistsAsync(dto.AuthorId);

        if (!authorExists)
            throw new NotFoundException(
                $"The author with the identifier {dto.AuthorId} does not exist");

        var categoryExists = await _categoryRepository.IsExistsAsync(dto.CategoryId);

        if (!categoryExists)
            throw new NotFoundException(
                $"The category with the identifier {dto.CategoryId} does not exist");

        var isbnExists = await _repository.IsExistByIsbnAsync(dto.ISBN);

        if (isbnExists && book.ISBN != dto.ISBN)
            throw new ConflictException(
                $"A book with the ISBN {dto.ISBN} already exists");

        book.Title = dto.Title;
        book.ISBN = dto.ISBN;
        book.PublishedDate = dto.PublishedDate;
        book.AuthorId = dto.AuthorId;
        book.CategoryId = dto.CategoryId;

        return await _repository.UpdateAsync(book);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        ValidateId(id);

        var exists = await _repository.IsExistsAsync(id);

        if (!exists)
            throw new NotFoundException(
                $"The book with the identifier {id} does not exist");

        var result = await _repository.DeleteAsync(id);

        return result switch
        {
            DeleteResult.Success => true,

            DeleteResult.HasDependencies => throw new ConflictException(
                "This book cannot be deleted because it has dependencies"),

            DeleteResult.SqlProblem => throw new Exception(
                "An error occurred while deleting the book"),

            _ => throw new Exception(
                "An unexpected error occurred while deleting the book")
        };
    }

    private static void ValidateId(int id)
    {
        if (id <= 0)
            throw new BadRequestException(
                "The identifier must be greater than zero");
    }
}