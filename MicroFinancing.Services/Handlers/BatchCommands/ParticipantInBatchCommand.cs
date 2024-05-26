using AutoMapper;
using MediatR;
using MicroFinancing.Core.Common;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Entities;
using MicroFinancing.Interfaces.Repositories;
using Syncfusion.Blazor;

namespace MicroFinancing.Services.Handlers.BatchCommands;

public class ParticipantInBatchCommand : IRequest<object>
{
    public DataManagerRequest DataManagerRequest { get; set; }
    public long BatchId { get; set; }
}

public class ParticipantInBatchCommandHandler : IRequestHandler<ParticipantInBatchCommand, object>
{
    private readonly IRepository<BatchInCustomer, long> _repository;
    private readonly IMapper _mapper;

    public ParticipantInBatchCommandHandler(IRepository<BatchInCustomer, long> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<object> Handle(ParticipantInBatchCommand request, CancellationToken cancellationToken)
    {
        var query = _repository.Entity.AsQueryable();
        if (request.BatchId != 0)
        {
            query = query.Where(c => c.BatchId == request.BatchId);
        }

        var dto = _mapper.ProjectTo<ParticipantsInBatchDto>(query);

        return await dto.ToDataResult(request.DataManagerRequest);
    }
}