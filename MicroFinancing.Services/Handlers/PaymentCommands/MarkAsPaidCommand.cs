using MediatR;

using MicroFinancing.Interfaces.Services;

namespace MicroFinancing.Services.Handlers.PaymentCommands;

public class MarkAsPaidCommand : IRequest
{
    public string CreatorId { get; set; }
    public long LendingId { get; set; }
    public decimal TotalCredit { get; set; }
    public long CustomerId { get; set; }
}

public class MarkAsPaidCommandHandler : IRequestHandler<MarkAsPaidCommand>
{
    private readonly IPaymentService _paymentService;

    public MarkAsPaidCommandHandler(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    public async Task Handle(MarkAsPaidCommand request,
                             CancellationToken cancellationToken)
    {
        await _paymentService.MarkAsPaid(request.LendingId,
                                         request.TotalCredit,
                                         request.CreatorId,
                                         request.CustomerId);
    }
}
