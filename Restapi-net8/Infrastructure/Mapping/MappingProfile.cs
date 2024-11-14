using AutoMapper;
using Restapi_net8.Model.Domain;
using Restapi_net8.Model.DTO.Category;
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
            CreateMap<UpdateUsers, Customer>();
        }
    }
}
