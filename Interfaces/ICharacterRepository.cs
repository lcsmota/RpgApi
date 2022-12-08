using RpgApi.DTOs;
using RpgApi.Models;

namespace RpgApi.Interfaces;

public interface ICharacterRepository
{
    Task<IEnumerable<Character>> GetCharactersAsync();
    Task<Character> GetCharacterByIdAsync(int id);
    Task AddCharacterAsync(Character character);
    void UpdateCharacter(Character character);
    void DeleteCharacter(Character character);

    Task<IEnumerable<Character>> GetCharactersWithClassAsync();
    Task<Character> GetCharacterByIdWithClassAsync(int id);
}
