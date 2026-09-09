using BookFlow.Core.Interfaces;
using BookFlow.Core.DTOs;

namespace BookFlow.BLL.Services;

public class CountryService : ICountryService
{
    private readonly ICountryRepository _repository;

    public CountryService(
        ICountryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CountryResponseDto>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }
}
