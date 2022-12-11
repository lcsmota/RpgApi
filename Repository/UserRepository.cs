using Microsoft.EntityFrameworkCore;
using RpgApi.Context;
using RpgApi.Interfaces;
using RpgApi.Models;

namespace RpgApi.Repository;

public class UserRepository : IUserRepository
{
    private readonly RPGDbContext _context;
    public UserRepository(RPGDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return (await _context.Users.AsNoTracking().FirstOrDefaultAsync(prop => prop.Id == id));
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return (await _context.Users.AsNoTracking().ToListAsync());
    }

    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public void DeleteUser(User user)
    {
        _context.Users.Remove(user);
    }

    public void UpdateUser(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
        _context.Update(user);
    }

    public async Task<User> AuthenticateAsync(User user)
    {
        var usr = await _context.Users
                      .AsNoTracking()
                      .Where(p => p.Login == user.Login && p.Password == user.Password)
                      .FirstOrDefaultAsync();
        return usr;
    }
}
