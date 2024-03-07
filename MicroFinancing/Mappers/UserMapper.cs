using AutoMapper;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Entities;

namespace MicroFinancing.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<ApplicationUser, CreateUpdateUserDTM>().ReverseMap();
            CreateMap<ApplicationUser, CreateUpdateUserDTM>()
                .ForMember(dst=>dst.UserId
                    ,opt=>opt.MapFrom(src=>src.Id));
        }
    }
}
