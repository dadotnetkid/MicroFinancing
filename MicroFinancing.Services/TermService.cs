using AutoMapper;
using MicroFinancing.Core.Common;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Entities;
using MicroFinancing.Interfaces.Repositories;
using Syncfusion.Blazor;

namespace MicroFinancing.Services;

public class TermService : ITermService
{
    private readonly IRepository<Term, long> _repository;
    private readonly IMapper _mapper;

    public TermService(IRepository<Term, long> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<object> TermGrid(DataManagerRequest dm)
    {
        var dto = _mapper.ProjectTo<TermDto>(_repository.Entity);

        return await dto.ToDataResult(dm);
    }
}