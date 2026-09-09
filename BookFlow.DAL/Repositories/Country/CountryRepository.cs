using BookFlow.Core.DTOs;
using BookFlow.Core.Entities;
using BookFlow.Core.Interfaces;
using BookFlow.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookFlow.DAL.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly AppDbContext _context;

    public CountryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CountryResponseDto>> GetAllAsync()
    {
        return await _context.Countries
            .AsNoTracking()
            .Select(CountryResponseProjection)
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    public async Task<bool> IsExistsAsync(int id)
    {
        return await _context.Countries
            .AnyAsync(x => x.Id == id);
    }

    private static readonly Expression<Func<Country, CountryResponseDto>> CountryResponseProjection =
    x => new CountryResponseDto
    {
        Id = x.Id,
        Name = x.Name,
        Code = x.Code
    };

}
