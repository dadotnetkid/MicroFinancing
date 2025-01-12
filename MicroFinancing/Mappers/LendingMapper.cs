using AutoMapper;

using MicroFinancing.DataTransferModel;
using MicroFinancing.Entities;

namespace MicroFinancing.Mappers;

public class LendingMapper : Profile
{

    public LendingMapper()
    {
        CreateMap<Lending, LendingGridDTM>()
            .ForMember(dst => dst.Amount
                       , opt => opt.MapFrom(src => src.Amount + src.ItemAmount))
            .ForMember(dst => dst.CustomerName
                       , opt => opt.MapFrom(src => $"{src.Customers.FirstName} {src.Customers.LastName}"))
            .ForMember(dst => dst.Collector
                       , opt => opt.MapFrom(src => src.CollectorUser.FirstName + " " + src.CollectorUser.LastName));
    }
}