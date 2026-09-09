using Microsoft.AspNetCore.Mvc;
using BookFlow.Core.DTOs;
using BookFlow.Core.Interfaces;

namespace BookFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _service;

    public BookController(
        IBookService service)
    {
        _service = service;
    }

    // GET: api/Book
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<BookResponseDto>>> GetAll()
    {
        var books = await _service.GetAllAsync();
        return Ok(books);
    }

    // POST: api/Book
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] BookCreateDto book)
    {
        var id = await _service.CreateAsync(book);
        return CreatedAtAction(
                nameof(GetById),
                new { id },
                new { id });
    }

    // GET: api/Book/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(BookResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _service.GetByIdAsync(id);
        return Ok(book);
    }

    // PUT: api/Book/5
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] BookUpdateDto book)
    {
        var result = await _service.UpdateAsync(book, id);
        return Ok(new
        {
            message = "Book updated successfully"
        });
    }

    // DELETE: api/Book/5
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
