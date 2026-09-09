using BookFlow.Core.DTOs;
using BookFlow.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorService _service;

    public AuthorController(
        IAuthorService service)
    {
        _service = service;
    }

    // GET: api/Authors
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AuthorResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<AuthorResponseDto>>> GetAll()
    {
        var authors = await _service.GetAllAsync();
        return Ok(authors);
    }

    // POST: api/Authors
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] AuthorCreateDto author)
    {
        var id = await _service.CreateAsync(author);
        return CreatedAtAction(
                nameof(GetById),
                new { id },
                new { id });
    }

    // GET: api/authors/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AuthorResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        var author = await _service.GetByIdAsync(id);
        return Ok(author);
    }

    // PUT: api/authors/5
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] AuthorUpdateDto author)
    {
            var result = await _service.UpdateAsync(author, id);
            return Ok(new
            {
                message = "Author updated successfully"
            });
    }

    // DELETE: api/authors/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

}
