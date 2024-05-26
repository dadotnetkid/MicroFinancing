using AutoMapper;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Entities;

namespace MicroFinancing.Mappers;

public class TermMapper : Profile
{
    public TermMapper()
    {
        CreateMap<Term, TermDto>();
    }
}