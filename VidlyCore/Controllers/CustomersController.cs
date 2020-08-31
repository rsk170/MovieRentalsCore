using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VidlyCore.Data;
using VidlyCore.Entities.Models;
using VidlyCore.Services;
using VidlyCore.Services.Dto;
using VidlyCore.ViewModels;

namespace VidlyCore.Controllers
{
    public class CustomersController : Controller
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

        public void InitializeModel(CustomerViewModel model)
        {
            model.MembershipTypes = _customers.GetMembershipTypes();
        }

        public IActionResult New()
        {
            var model = new CustomerViewModel();

            InitializeModel(model);

            return View("CustomerForm", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(CustomerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                InitializeModel(model);
                return View("CustomerForm", model);
            }

            var dto = _mapper.Map<CustomerViewModel, CustomerDto>(model);
            //var dto = Mapper.Map<CustomerViewModel, CustomerDto>(model);

            _customers.SaveCustomer(dto);

            return RedirectToAction("Index", "Customers");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            Customer customer = _customers.GetCustomer(id);

            if (customer == null)
                return NotFound();

            var model = _mapper.Map<Customer, CustomerViewModel>(customer);
            InitializeModel(model);

            return View("CustomerForm", model);
        }
    }
}
