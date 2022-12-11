using RpgApi.Models;

namespace RpgApi.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User> GetUserByIdAsync(int id);
    Task AddUserAsync(User user);
    void UpdateUser(User user);
    void DeleteUser(User user);

    Task<User> AuthenticateAsync(User user);
}
