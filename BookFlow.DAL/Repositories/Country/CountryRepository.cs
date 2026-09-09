using BookFlow.Core.DTOs;
using BookFlow.Core.Entities;
using BookFlow.Core.Interfaces;
using BookFlow.DAL.Context;
using Microsoft.EntityFrameworkCore;

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
            .Select(x => new CountryResponseDto
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code
            })
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    public async Task<bool> isExistAsync(int id)
    {
        return await _context.Countries
            .AnyAsync(x => x.Id == id);
    }

}
