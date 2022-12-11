using RpgApi.Context;
using RpgApi.Interfaces;

namespace RpgApi.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly RPGDbContext _context;
    private ICharacterRepository _characterRepo;
    private IRPGClassRepository _rpgClassRepo;
    private IUserRepository _userRepo;
    public UnitOfWork(RPGDbContext context)
    {
        _context = context;
    }

    public ICharacterRepository CharactersRepository
        => _characterRepo ??= new CharacterRepository(_context);

    public IRPGClassRepository RPGClassesRepository
        => _rpgClassRepo ??= new RPGClassRepository(_context);

    public IUserRepository UsersRepository
        => _userRepo ??= new UserRepository(_context);

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}
