using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VidlyCore.Data;
using VidlyCore.Entities.Models;
using VidlyCore.Services;
using VidlyCore.ViewModels;

namespace VidlyCore.Controllers
{
    public class MoviesController : Controller
    {
        private MoviesService _movies;

        private ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
            _movies = new MoviesService(_context);
        }

        public IActionResult Index()
        {
            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List");

            return View("ReadOnlyList");
        }

        public IActionResult Details(int id)
        {
            Movie movie = _movies.GetMovie(id);

            if (movie == null)
                return NotFound();

            return View(movie);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public IActionResult New()
        {
            var viewModel = new MovieFormViewModel
            {
                Genres = _movies.GetGenres()
            };

            return View("MovieForm", viewModel);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public IActionResult Edit(int id)
        {
            var movie = _movies.GetMovie(id);

            if (movie == null)
                return NotFound();

            var viewModel = InitializeViewModel(movie);

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                MovieFormViewModel viewModel = InitializeViewModel(movie);

                return View("MovieForm", viewModel);
            }

            _movies.SaveMovie(movie);

            return RedirectToAction("Index", "Movies");
        }

        private MovieFormViewModel InitializeViewModel(Movie movie)
        {
            return new MovieFormViewModel(movie)
            {
                Genres = _movies.GetGenres()
            };
        }
    }
}
