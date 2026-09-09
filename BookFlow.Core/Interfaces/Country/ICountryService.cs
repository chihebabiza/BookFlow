using BookFlow.Core.DTOs;
namespace BookFlow.Core.Interfaces;

public interface ICountryService
{
    Task<IEnumerable<CountryResponseDto>> GetAllAsync();
}
