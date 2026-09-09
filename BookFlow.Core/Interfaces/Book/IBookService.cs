using BookFlow.Core.DTOs;
namespace BookFlow.Core.Interfaces;

public interface IBookService
{
    Task<IEnumerable<BookResponseDto>> GetAllAsync();

    Task<BookResponseDto> GetByIdAsync(int id);

    Task<int> CreateAsync(BookCreateDto dto);

    Task<bool> UpdateAsync(BookUpdateDto dto, int id);

    Task<bool> DeleteAsync(int id);
}
