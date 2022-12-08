using Microsoft.EntityFrameworkCore;
using RpgApi.Context;
using RpgApi.Interfaces;
using RpgApi.Models;

namespace RpgApi.Repository;

public class RPGClassRepository : IRPGClassRepository
{
    private readonly RPGDbContext _context;
    public RPGClassRepository(RPGDbContext context)
    {
        _context = context;
    }

    public async Task<RpgClass> GetRpgClassByIdAsync(int id)
    {
        return await _context.RpgClasses.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<IEnumerable<RpgClass>> GetRpgClasses()
    {
        return await _context.RpgClasses.AsNoTracking().ToListAsync();
    }
}
