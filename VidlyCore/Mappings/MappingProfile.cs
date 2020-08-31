using AutoMapper;
using VidlyCore.Entities.Models;
using VidlyCore.Services.Dto;
using VidlyCore.ViewModels;

namespace VidlyCore.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<CustomerDto, Customer>().ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<CustomerViewModel, CustomerDto>();

            CreateMap<Customer, CustomerViewModel>();
            CreateMap<CustomerViewModel, Customer>();

        }
    }
}
