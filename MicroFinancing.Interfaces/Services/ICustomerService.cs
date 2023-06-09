using MicroFinancing.Core.Enumeration;
using MicroFinancing.DataTransferModel;

namespace MicroFinancing.Interfaces.Services
{
    public interface ICustomerService
    {
        IQueryable<CustomerGridDTM> GetCustomer();
        Task AddCustomer(CreateCustomerDTM model);
        Task<CustomerDetailDTM?> GetCustomerDetail(long id);
        Task UpdateFlag(long customerId, CustomerFlag customerFlagValue);
        Task<BaseAuthorizePermissionDTM> GetPermission();
        IQueryable<CustomerBaseDTM> GetCustomerByCollector(string collectorId);
    }
}