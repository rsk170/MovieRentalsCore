using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using VidlyCore.Data;
using VidlyCore.Services;
using VidlyCore.Services.Dto;

namespace VidlyCore.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private ApplicationDbContext _context;

        private CustomersService _customers;

        private IMapper _mapper;

        public CustomersController(IMapper mapper, ApplicationDbContext context)
        {
            _context = context;
            _mapper = mapper;
            _customers = new CustomersService(_context, _mapper);
        }

        [HttpGet]
        public IActionResult GetCustomers(string query = null)
        {
            var items = _customers.GetAllCustomers(query);
            return Ok(items);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            if (_customers.DeleteCustomer(id) == Result.NotFound)
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
        }
    }
}
