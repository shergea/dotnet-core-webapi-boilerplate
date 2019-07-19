using WebApi.Model;
using WebApi.Model.ViewModels.UserController;

namespace AutoMapper.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, GetOutputViewModel>().ReverseMap();
            CreateMap<User, RegisterOutputViewModel>().ReverseMap();
        }
    }
}