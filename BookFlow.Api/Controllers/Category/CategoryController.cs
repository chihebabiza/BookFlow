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

}
