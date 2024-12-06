using AutoMapper;

using MicroFinancing.Interfaces.Services;
using Syncfusion.Blazor;

namespace MicroFinancing.Services;

public sealed class BatchService : IBatchService
{
    private readonly IRepository<Batch, long> _batchRepository;
    private readonly IMapper _mapper;

    public BatchService(IRepository<Batch, long> batchRepository, IMapper mapper)
    {
        _batchRepository = batchRepository;
        _mapper = mapper;
    }
    public async Task<object> BatchGridAdaptor(DataManagerRequest dm)
    {
        var map = _mapper.ProjectTo<BatchDto>(_batchRepository.Entity);

        return await map.ToDataResult(dm);
    }
}