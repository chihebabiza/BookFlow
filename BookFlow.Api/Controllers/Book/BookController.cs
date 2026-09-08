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

}
