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
        public async Task<IEnumerable<Car>> GetAllAsync(CarFilter filter = null) 
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

         return await _context.Cars.ToListAsync();
        }
        public async Task<Car?> GetByIdAsync(int id)
        {
            return await _context.Cars.FindAsync(id);
        }
        public async Task AddAsync(Car car) 
        { 
         await _context.Cars.AddAsync(car);
          await  _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Car car)
        {
            var existingCar = await _context.Cars.FindAsync(car.CarId);
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
              await  _context.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(int id) 
        {
            var carNeedsToDelete = await _context.Cars.FindAsync(id);
            if (carNeedsToDelete != null) 
            {
             _context.Cars.Remove(carNeedsToDelete);
              await  _context.SaveChangesAsync();
            }
      
        }
    }
}
