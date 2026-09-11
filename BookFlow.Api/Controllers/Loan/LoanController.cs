using BookFlow.Core.DTOs;
using BookFlow.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoanController : ControllerBase
{
    private readonly ILoanService _service;

    public LoanController(
        ILoanService service)
    {
        _service = service;
    }

    // GET : api/member/1
    [HttpGet("member/{memberId:int}")]
    [ProducesResponseType(typeof(IEnumerable<LoanResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<LoanResponseDto>>> GetByMember(int memberId)
    {
        var loans = await _service.GetByMemberAsync(memberId);
        return Ok(loans);
    }

    // POST: api/Loans
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] LoanCreateDto loan)
    {
        var id = await _service.CreateAsync(loan);
        return CreatedAtAction(
                nameof(GetById),
                new { id },
                new { id });
    }

    // GET: api/Loans/5
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(LoanResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        var loan = await _service.GetByIdAsync(id);
        return Ok(loan);
    }

    // PUT: api/Loans/5
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] LoanUpdateDto loan)
    {
            var result = await _service.UpdateAsync(loan, id);
            return Ok(new
            {
                message = "Loan updated successfully"
            });
    }

    // DELETE: api/Loans/5
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
