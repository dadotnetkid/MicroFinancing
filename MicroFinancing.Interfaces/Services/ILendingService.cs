using MicroFinancing.DataTransferModel;

using Syncfusion.Blazor;

namespace MicroFinancing.Interfaces.Services;

public interface ILendingService
{
    IQueryable<LendingGridDTM> Get();
    Task AddLending(CreateLendingDTM model);
    Task<object> GetSummary(DataManagerRequest dm,
                            string userId);

    Task DeleteLending(long id);

    Task EditLending(EditLendingDTM model);

    EditLendingDTM GetLendingDetailsForEdit(long id);

    Task SetActiveLoan(long id);

    Task MarkAsPaid(long lendingId,
                    string creatorUserId);

    Task<List<LendingForApprovalGridDTM>> GetLendingNotApproved();

    Task AddLendingForApproval(CreateLendingForApprovalDTM model);

    Task Release(LendingForApprovalGridDTM? item);
}