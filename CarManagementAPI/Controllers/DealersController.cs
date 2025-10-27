using CarManagementAPI.Data;
using CarManagementAPI.Interfaces;
using CarManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealersController : ControllerBase
    {
        private readonly CarContext _context;
        public DealersController(CarContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Dealer>> GetDealers()
        {
            return _context.Dealers.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Dealer> GetDealer(int id)
        {
            var dealer = _context.Dealers.Find(id);

            if (dealer == null)
            {
                return NotFound();
            }

            return dealer;
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteDealer(int id)
        {
            var dealer = _context.Dealers.Find(id);
            if (dealer == null)
            {
                return NotFound();
            }

            _context.Dealers.Remove(dealer);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPost]
        public IActionResult AddDealer(Dealer dealer)
        {
            _context.Dealers.Add(dealer);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetDealer), new { id = dealer.DealerId }, dealer);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDealer(int id, Dealer dealer)
        {
            if (id != dealer.DealerId)  return BadRequest(); 
            _context.Update(dealer);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
