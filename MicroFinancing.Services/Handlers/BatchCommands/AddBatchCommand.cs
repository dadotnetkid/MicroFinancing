using AutoMapper;
using MediatR;

namespace MicroFinancing.Services.Handlers.BatchCommands;

public class AddBatchCommand : IRequest<BatchDto>
{
    public AddBatchDto Batch { get; set; }
}

public class AddBatchCommandHandler : IRequestHandler<AddBatchCommand, BatchDto>
{
    private readonly IRepository<Entities.Batch, long> _repository;
    private readonly IMapper _mapper;

    public AddBatchCommandHandler(IRepository<Entities.Batch, long> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BatchDto> Handle(AddBatchCommand request, CancellationToken cancellationToken)
    {
        var batch = new Entities.Batch()
        {
            Name = request.Batch.Name,
            TermId = request.Batch.Term,
            Amount = request.Batch.Amount,
            Participants = request.Batch.Participants,
            StartAt = request.Batch.StartAt,
            AdministratingUserId = request.Batch.AdministeringUser,
        };

        await _repository.AddAsync(batch);

        return _mapper.Map<BatchDto>(batch);
    }
}