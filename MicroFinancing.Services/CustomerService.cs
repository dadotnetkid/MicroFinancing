using Hangfire;
using MicroFinancing.Core.Common;
using MicroFinancing.Core.Enumeration;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Entities;
using MicroFinancing.Interfaces.Repositories;
using MicroFinancing.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace MicroFinancing.Services;

public class CustomerService : ICustomerService
{
    private readonly IRepository<Customers, long> _customerRepository;
    private readonly IRepository<Lending, long> _lendingRepository;
    private readonly IUserService _userService;
    private readonly ISmsService _smsService;

    public CustomerService(IRepository<Customers, long> customerRepository,
        IRepository<Lending, long> lendingRepository,
        IUserService userService,
        ISmsService smsService)
    {
        _customerRepository = customerRepository;
        _lendingRepository = lendingRepository;
        _userService = userService;
        _smsService = smsService;
    }

    public IQueryable<CustomerGridDTM> GetCustomer()
    {
        return _customerRepository.Entity.Select(x => new CustomerGridDTM
        {
            FirstName = x.FirstName,
            MiddleName = x.MiddleName,
            LastName = x.LastName,
            DateOfBirth = x.DateOfBirth,
            PlaceOfBirth = x.PlaceOfBirth,
            Address = x.Address,
            Id = x.Id,
            TotalAmountPaid = x.Payments.Sum(x => x.PaymentAmount),
            FullName = x.FullName,
        });
    }

    public async Task AddCustomer(CreateCustomerDTM model)
    {
        await _customerRepository.AddAsync(new Customers
        {
            Address = model.Address,
            DateOfBirth = model.DateOfBirth,
            FirstName = model.FirstName,
            LastName = model.LastName,
            MiddleName = model.MiddleName,
            PlaceOfBirth = model.PlaceOfBirth ?? string.Empty,
            PhoneNumber = model.PhoneNumber,
            IsDeleted = false
        });

        BackgroundJob.Enqueue(() =>
            _smsService.SendNewlyCreateCustomer(model.PhoneNumber,
                $"{model.FirstName} {model.LastName}"));
    }

    public async Task EditCustomer(EditCustomerDTM? model)
    {
        var customer = await _customerRepository.Entity.FindAsync(model.Id);

        if (customer == null)
        {
            throw new Exception("Customer not found");
        }

        customer.Address = model.Address;
        customer.DateOfBirth = model.DateOfBirth;
        customer.FirstName = model.FirstName;
        customer.LastName = model.LastName;
        customer.MiddleName = model.MiddleName;
        customer.PlaceOfBirth = model.PlaceOfBirth ?? string.Empty;
        customer.PhoneNumber = model.PhoneNumber;

        await _customerRepository.SaveChangesAsync();
    }

    public async Task<CustomerDetailDTM?> GetCustomerDetail(long id)
    {
        var res = await _customerRepository.Entity.Where(x => x.Id == id).Select(x => new CustomerDetailDTM
        {
            FirstName = x.FirstName,
            MiddleName = x.MiddleName,
            LastName = x.LastName,
            DateOfBirth = x.DateOfBirth,
            PlaceOfBirth = x.PlaceOfBirth,
            Address = x.Address,
            Id = x.Id,
            PhoneNumber = x.PhoneNumber,
            TotalAmountPaid = x.Payments.Sum(p => p.PaymentAmount),
            TotalDebt = x.Lending.Sum(l => l.TotalCredit),
            TotalBalance = x.Lending.Sum(l => l.TotalCredit) - x.Payments.Sum(p => p.PaymentAmount),
            CustomerFlag = x.Flag,
            HasActiveLoan = x.Lending.Any(x => !x.IsPaid && x.IsActive),
            DailyDueAmount = x.Lending
                .OrderBy(c => c.Id)
                .Select(c => c.DailyDueAmount)
                .LastOrDefault()
        }).FirstOrDefaultAsync();

        return res;
    }

    public async Task UpdateFlag(long customerId, CustomerFlag customerFlagValue)
    {
        var customer = _customerRepository.Entity.AsNoTracking().FirstOrDefault(x => x.Id == customerId);
        customer.Flag = customerFlagValue;
        await _customerRepository.UpdateAsync(customer);
    }

    public async Task<BaseAuthorizePermissionDTM> GetPermission()
    {
        return new BaseAuthorizePermissionDTM
        {
            CanAddLoan = await _userService.IsAuthorize(ClaimsConstant.Customer.AddLoan, false),
            CanAddPayment = await _userService.IsAuthorize(ClaimsConstant.Customer.AddPayment, false),
            CanOverridePayment = await _userService.IsAuthorize(ClaimsConstant.Customer.AddLoan, false),
            CanSetFlag = await _userService.IsAuthorize(ClaimsConstant.Customer.SetFlag, false),
            CanPrint = await _userService.IsAuthorize(ClaimsConstant.Customer.Print, false)
        };
    }

    public IQueryable<CustomerGridDTM> GetCustomerByCollector(string collectorId)
    {
        var customer = _lendingRepository.Entity.Where(x => x.Collector == collectorId && x.IsActive && !x.IsPaid)
            .Select(x => x.CustomerId)
            .Distinct().ToList();
        var customers = _customerRepository.Entity.Where(x => customer.Contains(x.Id))
            .Select(x => new CustomerGridDTM
            {
                FirstName = x.FirstName,
                MiddleName = x.MiddleName,
                LastName = x.LastName,
                DateOfBirth = x.DateOfBirth,
                PlaceOfBirth = x.PlaceOfBirth,
                Address = x.Address,
                Id = x.Id,
                TotalAmountPaid = x.Payments.Sum(x => x.PaymentAmount),
                FullName = x.FullName,
                TotalBalance = x.Lending.Sum(c => c.TotalCredit) - x.Payments.Sum(c => c.PaymentAmount)
            });
        return customers;
    }

    public async Task<EditCustomerDTM?> GetCustomerDetailForEdit(long customerId)
    {
        var result = await _customerRepository.Entity.Where(x => x.Id == customerId).Select(x => new EditCustomerDTM
        {
            Id = x.Id,
            FirstName = x.FirstName,
            MiddleName = x.MiddleName,
            LastName = x.LastName,
            DateOfBirth = x.DateOfBirth,
            PlaceOfBirth = x.PlaceOfBirth,
            Address = x.Address,
            PhoneNumber = x.PhoneNumber
        }).FirstOrDefaultAsync();

        return result;
    }

    public async Task DeleteCustomer(long contextId)
    {
        var customer = await _customerRepository.Entity.FindAsync(contextId);
        customer.IsDeleted = true;
        await _customerRepository.SaveChangesAsync();
    }
}