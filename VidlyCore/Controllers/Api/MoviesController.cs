using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VidlyCore.Data;
using VidlyCore.Entities.Models;
using VidlyCore.Services;
using VidlyCore.Services.Dto;

namespace VidlyCore.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private ApplicationDbContext _context;

        private MoviesService _movies;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
            _movies = new MoviesService(_context);
        }

        public IActionResult GetMovies(string query = null)
        {
            var items = _movies.GetAllMovies(query);
            return Ok(items);
        }

        [HttpDelete]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IActionResult DeleteMovie(int id)
        {
            if (_movies.DeleteMovie(id) == Result.NotFound)
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
