using Microsoft.AspNetCore.Mvc;
using BookFlow.Core.DTOs;
using BookFlow.Core.Interfaces;

namespace BookFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoryController(
        ICategoryService service)
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

    // POST: api/Category
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] CategoryCreateDto category)
    {
        var id = await _service.CreateAsync(category);
        return CreatedAtAction(
                nameof(GetById),
                new { id },
                new { id });
    }

    // GET: api/Category/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CategoryResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _service.GetByIdAsync(id);
        return Ok(book);
    }

    // PUT: api/Category/5
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] CategoryUpdateDto category)
    {
        var result = await _service.UpdateAsync(category, id);
        return Ok(new
        {
            message = "Category updated successfully"
        });
    }

    // DELETE: api/Category/5
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
