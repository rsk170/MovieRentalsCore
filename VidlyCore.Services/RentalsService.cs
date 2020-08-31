using System;
using System.Collections.Generic;
using System.Linq;
using VidlyCore.Data;
using VidlyCore.Entities.Models;
using VidlyCore.Services.Dto;

namespace VidlyCore.Services
{
    public class RentalsService
    {
        private ApplicationDbContext _context;

        public RentalsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public RentalResult CreateRental(NewRentalDto newRental)
        {
            Customer customer = _context.Customers.Single(
               c => c.Id == newRental.CustomerId);
            List<Movie> movies = _context.Movies.Where(
                m => newRental.MovieIds.Contains(m.Id)).ToList();

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                {
                    return RentalResult.NotAvailable;
                }

                movie.NumberAvailable--;

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateAdded = DateTime.Now,
                };

                _context.Rentals.Add(rental);
            }

            _context.SaveChanges();

            return RentalResult.Success;
        }
    }
}
