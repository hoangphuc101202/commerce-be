using AutoMapper;
using Restapi_net8.Model.Domain;
using Restapi_net8.Model.DTO.Category;

namespace Restapi_net8.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDTO>();
            CreateMap<CreateCategoryRequestDTO, Category>();
            CreateMap<UpdateCategoryDTO, Category>();
        }
    }
}
