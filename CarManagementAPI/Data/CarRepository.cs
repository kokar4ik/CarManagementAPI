using CarManagementAPI.Data;
using CarManagementAPI.Interfaces;
using CarManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarManagementAPI.Data
{
    public class CarRepository:ICarRepository
    {
        private readonly CarContext _context;
        public CarRepository(CarContext context)
        {
            _context = context;
        }
        public IEnumerable<Car> GetAll(CarFilter filter = null) 
        {
            var query = _context.Cars.AsQueryable();

            if (filter == null)
                return query.ToList();

            if (filter.IsAvailable.HasValue)
                query = query.Where(c => c.IsAvailable == filter.IsAvailable.Value);

            if (!string.IsNullOrEmpty(filter.Brand))
                query = query.Where(c => c.Brand == filter.Brand);

            if (filter.MinYear.HasValue)
                query = query.Where(c => c.Year >= filter.MinYear.Value);

            if (filter.MaxYear.HasValue)
                query = query.Where(c => c.Year <= filter.MaxYear.Value);

            if (!string.IsNullOrEmpty(filter.SearchModel))
                query = query.Where(c => c.Model.Contains(filter.SearchModel));

            if (filter.MinPrice.HasValue)
                query = query.Where(c => c.Price >= filter.MinPrice.Value);

            if (filter.MaxPrice.HasValue)
                query = query.Where(c => c.Price <= filter.MaxPrice.Value);

            return query.ToList();
        }
        public Car GetById(int id)
        {
            return _context.Cars.Find(id);
        }
        public void Add(Car car) 
        { 
        _context.Cars.Add(car);
            _context.SaveChanges();
        }
        public void Update(Car car)
        {
            var existingCar = _context.Cars.Find(car.CarId);
            if (existingCar != null) 
            {
            existingCar.CarId = car.CarId;
            existingCar.Brand = car.Brand;
            existingCar.Model = car.Model;
            existingCar.Year = car.Year;
            existingCar.Price = car.Price;
            existingCar.IsAvailable = car.IsAvailable;
            existingCar.Mileage = car.Mileage;
            //    _context.Entry(existingCar).CurrentValues.SetValues(car);
                _context.SaveChanges();
            }
        }
        public void Delete(int id) 
        {
            var carNeedsToDelete = _context.Cars.Find(id);
            if (carNeedsToDelete != null) 
            {
                _context.Cars.Remove(carNeedsToDelete);
                _context.SaveChanges();
            }
      
        }
    }
}
