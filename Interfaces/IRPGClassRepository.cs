using RpgApi.Models;

namespace RpgApi.Interfaces;

public interface IRPGClassRepository
{
    Task<IEnumerable<RpgClass>> GetRpgClasses();
    Task<RpgClass> GetRpgClassByIdAsync(int id);
}
