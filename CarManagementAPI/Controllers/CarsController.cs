using CarManagementAPI.Interfaces;
using CarManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarRepository _repository;

        public CarsController(ICarRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Car>> GetCars(
            [FromQuery] int? dealerId = null,
    [FromQuery] string? dealerName = null,
     [FromQuery] bool? isAvailable = null,    
     [FromQuery] string brand = null,          
     [FromQuery] int? minYear = null,           
     [FromQuery] int? maxYear = null,           
     [FromQuery] string search = null,          
     [FromQuery] decimal? minPrice = null,      
     [FromQuery] decimal? maxPrice = null)      
        {
            var cars = _repository.GetAll();
          
            if (dealerId.HasValue)
            {
                cars = cars.Where(c => c.DealerId == dealerId.Value);
            }
            if (!string.IsNullOrEmpty(dealerName))
            
                cars = cars.Where(c => c.Dealer != null &&
                                      c.Dealer.Name.Contains(dealerName, StringComparison.OrdinalIgnoreCase));

            if (isAvailable.HasValue)
                cars = cars.Where(c => c.IsAvailable == isAvailable.Value);

            if (!string.IsNullOrEmpty(brand))
                cars = cars.Where(c => c.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase));

            if (minYear.HasValue)
                cars = cars.Where(c => c.Year >= minYear.Value);

            if (maxYear.HasValue)
                cars = cars.Where(c => c.Year <= maxYear.Value);

            if (!string.IsNullOrEmpty(search))
                cars = cars.Where(c => c.Model.Contains(search, StringComparison.OrdinalIgnoreCase));

            if (minPrice.HasValue)
                cars = cars.Where(c => c.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                cars = cars.Where(c => c.Price <= maxPrice.Value);

            return Ok(cars);
        }
        [HttpGet("{id}")]
        public ActionResult<Car> GetCar(int id) 
        {
            var car = _repository.GetById(id);
            if (car == null) 
            {
            return NotFound();
            }
            return Ok(car);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateCar(int id, Car car)
        {
            if (id != car.CarId) return BadRequest();
            _repository.Update(car);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCar(int id) 
        {
            _repository.Delete(id);
            return NotFound();
        }
        [HttpPost]
        public IActionResult CreateCar(Car car) 
        { 
        _repository.Add(car);
            return NoContent();
        }

    }
}
