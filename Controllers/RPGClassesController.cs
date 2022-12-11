using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RpgApi.Interfaces;

namespace RpgApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RPGClassesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    public RPGClassesController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult> GetClassesAsync()
    {
        try
        {
            var classes = await _unitOfWork.RPGClassesRepository.GetRpgClasses();

            return Ok(classes);
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("{id:int}")]
    [AllowAnonymous]
    public async Task<ActionResult> GetClasseByIdAsync(int id)
    {
        try
        {
            var classe = await _unitOfWork.RPGClassesRepository.GetRpgClassByIdAsync(id);

            if (classe is null) return NotFound("Class not found.");

            return Ok(classe);
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
