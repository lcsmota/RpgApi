using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RpgApi.DTOs;
using RpgApi.Interfaces;
using RpgApi.Models;

namespace RpgApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CharactersController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    public CharactersController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult> GetCharactersAsync()
    {
        try
        {
            var characters = await _unitOfWork.CharactersRepository.GetCharactersAsync();

            return Ok(characters);
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("{id:int}", Name = "GetCharacterById")]
    [AllowAnonymous]
    public async Task<ActionResult> GetCharacterAsync(int id)
    {
        try
        {
            var character = await _unitOfWork.CharactersRepository.GetCharacterByIdAsync(id);

            if (character is null) return NotFound("Character not found.");

            return Ok(character);
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    [Authorize(Roles = "standard, admin")]
    public async Task<ActionResult> AddCharacterAsync(CharacterForCreationDTO character)
    {
        try
        {
            if (character is null) return BadRequest("Check the field(s) and try again.");

            var createdCharacter = new Character
            {
                Name = character.Name,
                Race = character.Race,
                Height = character.Height,
                Weight = character.Weight,
                RpgClassId = character.RpgClassId
            };

            await _unitOfWork.CharactersRepository.AddCharacterAsync(createdCharacter);

            await _unitOfWork.CommitAsync();

            return CreatedAtRoute("GetCharacterById", new { id = createdCharacter.Id }, createdCharacter);
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> UpdateCharacterAsync(int id, CharacterForUpdateDTO character)
    {
        try
        {
            if (character.Id != id) return BadRequest("Check the field(s) and try again.");

            var createdCharacter = new Character
            {
                Id = character.Id,
                Name = character.Name,
                Race = character.Race,
                Height = character.Height,
                Weight = character.Weight,
                RpgClassId = character.RpgClassId
            };

            _unitOfWork.CharactersRepository.UpdateCharacter(createdCharacter);

            await _unitOfWork.CommitAsync();

            return NoContent();
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> DeleteCharacterAsync(int id)
    {
        try
        {
            var character = await _unitOfWork.CharactersRepository.GetCharacterByIdAsync(id);

            if (character is null) return NotFound("Character not found.");

            _unitOfWork.CharactersRepository.DeleteCharacter(character);

            await _unitOfWork.CommitAsync();

            return NoContent();
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }



    [HttpGet("withclass")]
    [Authorize(Roles = "standard, admin")]
    public async Task<ActionResult> GetCharactersWithClassAsync()
    {
        try
        {
            var characters = await _unitOfWork.CharactersRepository.GetCharactersWithClassAsync();

            return Ok(characters);
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("{id:int}/withclass")]
    [Authorize(Roles = "standard, admin")]
    public async Task<ActionResult> GetCharacterByIdWithClassAsync(int id)
    {
        try
        {
            var character = await _unitOfWork.CharactersRepository.GetCharacterByIdWithClassAsync(id);

            if (character is null) return NotFound("Character not found.");

            return Ok(character);
        }
        catch (System.Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}