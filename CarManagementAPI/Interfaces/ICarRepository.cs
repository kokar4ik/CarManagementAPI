using CarManagementAPI.Models;

namespace CarManagementAPI.Interfaces
{
    public interface ICarRepository
    {
       Task<IEnumerable<Car>> GetAllAsync(CarFilter filter = null);
      Task AddAsync(Car car);
        Task UpdateAsync(Car car);
        Task DeleteAsync(int id);
        Task<Car?> GetByIdAsync(int id);

    }
}
