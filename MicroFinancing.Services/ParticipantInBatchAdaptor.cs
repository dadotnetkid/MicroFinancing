using MediatR;
using MicroFinancing.Core.Common;
using MicroFinancing.Services.Handlers.BatchCommands;
using Syncfusion.Blazor;

namespace MicroFinancing.Services;

public class ParticipantInBatchAdaptor : DataAdaptor
{
    private readonly IMediator _mediator;

    public ParticipantInBatchAdaptor(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override Task<object> ReadAsync(DataManagerRequest dm, string additionalParam = null)
    {
        long batchId = 0;

        if (dm.Params != null && dm.Params.Any(x => x.Key == "BatchId"))
        {
            batchId = dm.Params.FirstOrDefault(x => x.Key == "BatchId").Value.ToTypeOf<long>();
        }
        return _mediator.Send(new ParticipantInBatchCommand()
        {
            DataManagerRequest = dm,
            BatchId = batchId
        });
    }
}