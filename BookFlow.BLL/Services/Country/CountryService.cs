using BookFlow.Core.Interfaces;
using BookFlow.Core.DTOs;
using BookFlow.BLL.Exceptions;

namespace BookFlow.BLL.Services;

public class CountryService : ICountryService
{
    private readonly ICountryRepository _repository;

    public CountryService(
        ICountryRepository repository)
    {
        _repository = repository;
    }

}
