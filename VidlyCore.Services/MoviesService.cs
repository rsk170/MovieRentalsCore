using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using VidlyCore.Data;
using VidlyCore.Entities.Models;
using VidlyCore.Services.Dto;

namespace VidlyCore.Services
{
    public class MoviesService
    {
        private ApplicationDbContext _context;

        public MoviesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Movie GetMovie(int id)
        {
            return _context.Movies.Include(m => m.Genre).FirstOrDefault(m => m.Id == id);
        }

        public List<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }

        public void SaveMovie(Movie movie)
        {
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                movie.NumberAvailable = movie.NumberInStock;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = GetMovie(movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
            }

            _context.SaveChanges();
        }

        public List<MovieListingItem> GetAllMovies(string query = null)
        {
            var moviesQuery = _context.Movies
                .Include(m => m.Genre)
                .Where(m => m.NumberAvailable > 0);

            if (!string.IsNullOrWhiteSpace(query))
            {
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));
            }

            var movieListingItem = moviesQuery.Select(m => new MovieListingItem()
            {
                Id = m.Id,
                Name = m.Name,
                Genre = m.Genre.Name,
            });

            return movieListingItem.ToList();
        }

        public Result DeleteMovie(int id)
        {
            var movie = GetMovie(id);
            if (movie == null)
                return Result.NotFound;

            _context.Movies.Remove(movie);
            _context.SaveChanges();
            return Result.Success;
        }
    }
}
