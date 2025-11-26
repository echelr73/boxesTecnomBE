using BoxesTecnom.Models;

namespace BoxesTecnom.Services
{
    public interface IWorkshopsService
    {
        Task<IEnumerable<Workshop>?> GetWorkshopsAsync();
    }
}
