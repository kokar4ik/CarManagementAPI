using CarManagementAPI.Models;

namespace CarManagementAPI.Interfaces
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetAll(CarFilter filter = null);
        void Add(Car car);
        void Update(Car car);
        void Delete(int id);
        Car GetById(int id);

    }
}
