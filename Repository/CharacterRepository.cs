using Microsoft.EntityFrameworkCore;
using RpgApi.Context;
using RpgApi.Interfaces;
using RpgApi.Models;

namespace RpgApi.Repository;

public class CharacterRepository : ICharacterRepository
{
    private readonly RPGDbContext _rpgDbContext;
    public CharacterRepository(RPGDbContext rpgDbContext)
    {
        _rpgDbContext = rpgDbContext;
    }

    public async Task<Character> GetCharacterByIdAsync(int id)
    {
        return await _rpgDbContext.Characters
                                  .AsNoTracking()
                                  .FirstOrDefaultAsync(prop => prop.Id == id);
    }

    public async Task<IEnumerable<Character>> GetCharactersAsync()
    {
        return await _rpgDbContext.Characters
                                  .AsNoTracking()
                                  .ToListAsync();
    }
    public async Task AddCharacterAsync(Character character)
    {
        await _rpgDbContext.Characters.AddAsync(character);
    }

    public void DeleteCharacter(Character character)
    {
        _rpgDbContext.Characters.Remove(character);
    }

    public void UpdateCharacter(Character character)
    {
        _rpgDbContext.Entry(character).State = EntityState.Modified;
        _rpgDbContext.Update(character);
    }

    public async Task<IEnumerable<Character>> GetCharactersWithClassAsync()
    {
        return await _rpgDbContext.Characters
                                  .Include(e => e.RpgClass)
                                  .AsNoTracking()
                                  .ToListAsync();
    }

    public async Task<Character> GetCharacterByIdWithClassAsync(int id)
    {
        return await _rpgDbContext.Characters
                                  .Include(e => e.RpgClass)
                                  .AsNoTracking()
                                  .FirstOrDefaultAsync(prop => prop.Id == id);
    }
}
