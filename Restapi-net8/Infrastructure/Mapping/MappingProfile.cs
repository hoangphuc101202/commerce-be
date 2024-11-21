using AutoMapper;
using Restapi_net8.Model.Domain;
using Restapi_net8.Model.DTO.Category;
using Restapi_net8.Model.DTO.Product;
using Restapi_net8.Model.DTO.Users;

namespace Restapi_net8.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCategoryRequestDTO, Category>();
            CreateMap<UpdateCategoryDTO, Category>();
            CreateMap<CreateUsers, Customer>();
            CreateMap<Customer, UserInfo>();
            CreateMap<UpdateUsers, Customer>().ForMember(dest => dest.BirthOfDate, opt => opt.MapFrom(src => 
                string.IsNullOrEmpty(src.birthOfDate) ? (DateTime?)null : DateTime.Parse(src.birthOfDate)));
            CreateMap<CreateProductRequestDTO, Product>().ForMember(dest => dest.ProductDate, opt => opt.MapFrom(src => 
                string.IsNullOrEmpty(src.productDate) ? (DateTime?)null : DateTime.Parse(src.productDate)))
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore());


            CreateMap<UpdateProductRequestDTO, Product>().ForMember(dest => dest.ProductDate, opt => opt.MapFrom(src => 
                string.IsNullOrEmpty(src.productDate) ? (DateTime?)null : DateTime.Parse(src.productDate)))
                .ForMember(dest => dest.CategoryId, opt => opt.Ignore());;
        }
    }
}
