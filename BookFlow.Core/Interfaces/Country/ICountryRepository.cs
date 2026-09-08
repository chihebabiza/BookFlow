using BookFlow.Core.Entities;

namespace BookFlow.Core.Interfaces;

public interface ICountryRepository
{
    Task<IEnumerable<Country>> GetAllAsync();
}
