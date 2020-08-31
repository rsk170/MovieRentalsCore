using Microsoft.AspNetCore.Mvc;
using VidlyCore.Data;
using VidlyCore.Services;
using VidlyCore.Services.Dto;

namespace VidlyCore.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewRentalsController : ControllerBase
    {
        private ApplicationDbContext _context;
        private RentalsService _rentals;

        public NewRentalsController(ApplicationDbContext context)
        {
            _context = context;
            _rentals = new RentalsService(_context);
        }

        [HttpPost]
        public IActionResult CreateNewRental([FromForm] NewRentalDto newRental)
        {
            if (_rentals.CreateRental(newRental) == RentalResult.NotAvailable)
            {
                return BadRequest("Movie is not available.");
            }
            else
            {
                return Ok();
            }
        }
    }
}
