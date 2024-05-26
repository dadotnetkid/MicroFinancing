using AutoMapper;
using MediatR;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MicroFinancing.Services.Handlers.BatchCommands;

public class GetBatchCommand : IRequest<BatchDto>
{
    public long Id { get; set; }
}

public class GetBatchCommandHandler : IRequestHandler<GetBatchCommand, BatchDto>
{
    private readonly IRepository<Entities.Batch, long> _repository;
    private readonly IMapper _mapper;

    public GetBatchCommandHandler(IRepository<Entities.Batch, long> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BatchDto?> Handle(GetBatchCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.ProjectTo<BatchDto>(_repository.Entity.Where(c => c.Id == request.Id));

        return await entity.FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
}