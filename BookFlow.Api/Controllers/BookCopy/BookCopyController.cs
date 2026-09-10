using Microsoft.AspNetCore.Mvc;
using BookFlow.Core.Interfaces;

namespace BookFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookCopyController : ControllerBase
{
    private readonly IBookCopyService _service;

    public BookCopyController(
        IBookCopyService service)
    {
        _service = service;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(List<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCopyNumbersByBook(int id)
    {
        var copyNumbers = await _service.GetCopyNumbersAsync(id);
        return Ok(copyNumbers);
    }

}
