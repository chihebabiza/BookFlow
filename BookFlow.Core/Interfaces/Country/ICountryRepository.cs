using BookFlow.Core.DTOs;

namespace BookFlow.Core.Interfaces;

public interface ICountryRepository
{
    Task<IEnumerable<CountryResponseDto>> GetAllAsync();

    Task<bool> IsExistsAsync(int id);
}
