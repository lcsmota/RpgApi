namespace RpgApi.Interfaces;

public interface IUnitOfWork
{
    public ICharacterRepository CharactersRepository { get; }
    public Task CommitAsync();
}
