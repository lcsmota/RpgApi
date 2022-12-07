using RpgApi.Context;
using RpgApi.Interfaces;

namespace RpgApi.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly RPGDbContext _context;
    private ICharacterRepository _characterRepo;
    public UnitOfWork(RPGDbContext context)
    {
        _context = context;
    }

    public ICharacterRepository CharactersRepository
        => _characterRepo ??= new CharacterRepository(_context);

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}
