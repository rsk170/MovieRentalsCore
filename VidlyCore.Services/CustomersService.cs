using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using VidlyCore.Data;
using VidlyCore.Entities.Models;
using VidlyCore.Services.Dto;

namespace VidlyCore.Services
{
    public class CustomersService
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public CustomersService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Customer GetCustomer(int id)
        {
            return _context.Customers.FirstOrDefault(c => c.Id == id);
        }

        public List<MembershipType> GetMembershipTypes()
        {
            return _context.MembershipTypes.ToList();
        }

        public List<CustomerListingItem> GetAllCustomers(string query = null)
        {
            var customersQuery = _context.Customers
                .Include(c => c.MembershipType)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                customersQuery = customersQuery.Where(
                    c => c.Name.Contains(query));
            }

            var customerListingItem = customersQuery.Select(c => new CustomerListingItem()
            {
                Id = c.Id,
                Name = c.Name,
                MembershipType = c.MembershipType.Name,
            });

            return customerListingItem.ToList();
        }

        public Result SaveCustomer(CustomerDto model)
        {
            Customer customer;

            if (model.Id == 0)
            {
                customer = new Customer();
                _mapper.Map(model, customer);
                _context.Customers.Add(customer);
            }
            else
            {
                if (!GetAllCustomers().Any(m => m.Id == model.Id))
                {
                    return Result.NotFound;
                }

                customer = GetCustomer(model.Id);
                _mapper.Map(model, customer);
            }

            _context.SaveChanges();
            return Result.Success;
        }

        public Result DeleteCustomer(int id)
        {
            var customer = GetCustomer(id);
            if (customer == null)
            {
                return Result.NotFound;
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return Result.Success;
        }
    }
}
