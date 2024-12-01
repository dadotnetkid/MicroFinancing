using AutoMapper;

using Syncfusion.Blazor;

namespace MicroFinancing.WebAssembly.Mappers;

public class DataManagerRequestMapper : Profile
{
    /*public DataManagerRequestMapper()
    {
        CreateMap<DataManagerRequest, MicroFinancing.WebAssembly.Services.Client.DataManagerRequest>()
            .ForMember(dst => dst.Skip, opt => opt.MapFrom(src => src.Skip))
            .ForMember(dst => dst.Take, opt => opt.MapFrom(src => src.Take))
            .ForMember(dst => dst.Where, opt => opt.MapFrom(src => src.Where))
            .ForMember(dst => dst.Search, opt => opt.MapFrom(src => src.Search))
            .ForMember(dst => dst.Sorted, opt => opt.MapFrom(src => src.Sorted))
            .ForMember(dst => dst.Group, opt => opt.MapFrom(src => src.Group))
            .ForMember(dst => dst.Aggregates, opt => opt.MapFrom(src => src.Aggregates));

        CreateMap<MicroFinancing.WebAssembly.Services.Client.DataManagerRequest, DataManagerRequest>()
            .ForMember(dst => dst.Skip, opt => opt.MapFrom(src => src.Skip))
            .ForMember(dst => dst.Take, opt => opt.MapFrom(src => src.Take))
            .ForMember(dst => dst.Where, opt => opt.MapFrom(src => src.Where))
            .ForMember(dst => dst.Search, opt => opt.MapFrom(src => src.Search))
            .ForMember(dst => dst.Sorted, opt => opt.MapFrom(src => src.Sorted))
            .ForMember(dst => dst.Group, opt => opt.MapFrom(src => src.Group))
            .ForMember(dst => dst.Aggregates, opt => opt.MapFrom(src => src.Aggregates));
    }*/
}
