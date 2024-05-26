using MediatR;
using MicroFinancing.Entities;
using MicroFinancing.Interfaces.Repositories;

namespace MicroFinancing.Services.Handlers.BatchCommands;

public class AddParticipantInBatchCommand : IRequest<bool>
{
    public long CustomerId { get; set; }
    public long BatchId { get; set; }
}

public class AddParticipantInBatchCommandHandler : IRequestHandler<AddParticipantInBatchCommand, bool>
{
    private readonly IRepository<BatchInCustomer, long> _repository;

    public AddParticipantInBatchCommandHandler(IRepository<BatchInCustomer, long> repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(AddParticipantInBatchCommand request, CancellationToken cancellationToken)
    {
        var query = _repository.Entity.Where(c => c.BatchId == request.BatchId);

        var index = query.Any() ? query.Max(c => c.Index) + 1 : 1;

        await _repository.AddAsync(new BatchInCustomer()
        {
            BatchId = request.BatchId,
            CustomerId = request.CustomerId,
            Index = index
        });

        return true;
    }
}