using Microsoft.AspNetCore.Mvc;
using BookFlow.Core.DTOs;
using BookFlow.Core.Interfaces;

namespace BookFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountryController : ControllerBase
{
    private readonly ICountryService _service;

    public CountryController(
        ICountryService service)
    {
        _service = service;
    }

    // GET: api/Authors
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CountryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<CountryResponseDto>>> GetAll()
    {
        var countries = await _service.GetAllAsync();
        return Ok(countries);
    }
}
