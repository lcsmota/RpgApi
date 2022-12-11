namespace RpgApi.Interfaces;

public interface IUnitOfWork
{
    public ICharacterRepository CharactersRepository { get; }
    public IRPGClassRepository RPGClassesRepository { get; }
    public IUserRepository UsersRepository { get; }
    public Task CommitAsync();
}
