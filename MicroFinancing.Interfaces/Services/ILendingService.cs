using MicroFinancing.DataTransferModel;

namespace MicroFinancing.Interfaces.Services;

public interface ILendingService
{
    IQueryable<LendingGridDTM> Get();
    Task AddLending(CreateLendingDTM model);
    IQueryable<LendingSummaryGridDTM> GetSummary();
}