using MicroFinancing.Core.Enumeration;
using MicroFinancing.DataTransferModel;

namespace MicroFinancing.Interfaces.Services;

public interface ICustomerService
{
    IQueryable<CustomerGridDTM> GetCustomer();
    Task AddCustomer(CreateCustomerDTM model);
    Task EditCustomer(EditCustomerDTM? model);
    Task<CustomerDetailDTM?> GetCustomerDetail(long id);
    Task UpdateFlag(long customerId, CustomerFlag customerFlagValue);
    Task<BaseAuthorizePermissionDTM> GetPermission();
    IQueryable<CustomerGridDTM> GetCustomerByCollector(string collectorId);

    Task<EditCustomerDTM?> GetCustomerDetailForEdit(long customerId);

    Task DeleteCustomer(long contextId);

    decimal? GetCustomerBalance(long dataId);
}