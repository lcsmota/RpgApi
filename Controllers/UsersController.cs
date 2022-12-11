using Microsoft.AspNetCore.Mvc;
using RpgApi.Interfaces;
using RpgApi.Models;
using RpgApi.Services;

namespace RpgApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    public UsersController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpPost("login")]
    public async Task<ActionResult<dynamic>> AuthenticateAsync(User user)
    {
        var us = await _unitOfWork.UsersRepository.AuthenticateAsync(user);

        if (us is null) return BadRequest("Usuário ou senha inválidos!");

        var token = TokenService.GenerateToken(us);

        us.Password = string.Empty;

        return new
        {
            user = us,
            token = token
        };
    }

    [HttpGet]
    public async Task<ActionResult> GetUsersAsync()
    {
        try
        {
            var users = await _unitOfWork.UsersRepository.GetUsersAsync();

            return Ok(users);
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("{id:int}", Name = "GetUserById")]
    public async Task<ActionResult> GetUserAsync(int id)
    {
        try
        {
            var user = await _unitOfWork.UsersRepository.GetUserByIdAsync(id);

            if (user is null) return NotFound("User not found.");

            return Ok(user);
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateUserAsync(User user)
    {
        try
        {
            user.Role = "standard";

            await _unitOfWork.UsersRepository.AddUserAsync(user);

            await _unitOfWork.CommitAsync();

            user.Password = string.Empty;

            return Ok(user);
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateUserAsync(int id, User user)
    {
        try
        {
            if (user.Id != id) return BadRequest("Check the field(s) and try again.");

            var dbUser = await _unitOfWork.UsersRepository.GetUserByIdAsync(id);
            if (dbUser is null) return NotFound("User not found.");

            user.Role = "standard";

            _unitOfWork.UsersRepository.UpdateUser(user);

            await _unitOfWork.CommitAsync();

            return NoContent();
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteUserAsync(int id)
    {
        try
        {
            var dbUser = await _unitOfWork.UsersRepository.GetUserByIdAsync(id);
            if (dbUser is null) return NotFound("User not found.");

            _unitOfWork.UsersRepository.DeleteUser(dbUser);

            await _unitOfWork.CommitAsync();

            return NoContent();
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}