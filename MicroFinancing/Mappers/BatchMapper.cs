using AutoMapper;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Entities;

namespace MicroFinancing.Mappers;

public class BatchMapper : Profile
{
    public BatchMapper()
    {
        CreateMap<Batch, BatchDto>()
            .ForMember(c => c.Term,
                opt =>
                                                                        opt.MapFrom(src => src.Term.Name))
            .ForMember(c => c.AdministeringUser,
                                                        opt => opt.MapFrom(src => src.AdministratingUser.FullName))
            .ForMember(c => c.TotalAddedParticipants,
                                                                        opt => opt.MapFrom(src => src.Customers.Count()));

        CreateMap<BatchInCustomer, ParticipantsInBatchDto>()
            .ForMember(dst => dst.Name,
                opt => opt.MapFrom(src => src.Customers.FullName));



    }
}