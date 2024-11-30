using Hangfire;
using MediatR;

using MicroFinancing.Core.Common;
using MicroFinancing.Core.Enumeration;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Entities;
using MicroFinancing.Interfaces.Repositories;
using MicroFinancing.Interfaces.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

using Syncfusion.Blazor;
using Syncfusion.Blazor.Inputs;

namespace MicroFinancing.Services;

public sealed class PaymentService : IPaymentService
{
    private readonly IHostEnvironment _hostEnvironment;
    private readonly IRepository<Lending, long> _lendingRepository;
    private readonly IMediator _mediator;
    private readonly ISmsService _smsService;
    private readonly IRepository<ApplicationUser, string> _userRepository;
    private readonly IRepository<Payment, long> _repository;
    private readonly IUserService _userService;

    public PaymentService(IRepository<Payment, long> repository,
        IRepository<Lending, long> lendingRepository,
        IHostEnvironment hostEnvironment,
        IUserService userService,
        IMediator mediator,
        ISmsService smsService,
        IRepository<ApplicationUser, string> userRepository)
    {
        _repository = repository;
        _lendingRepository = lendingRepository;
        _hostEnvironment = hostEnvironment;
        _userService = userService;
        _mediator = mediator;
        _smsService = smsService;
        _userRepository = userRepository;
    }

    public IQueryable<PaymentGridDTM> Get()
    {
        return _repository.Entity.Select(x => new PaymentGridDTM
        {
            CreatedAt = x.CreatedAt,
            CreatedBy = x.Creator.FullName,
            CustomerName = x.Customers.FullName,
            CustomerId = x.CustomerId,
            Id = x.Id,
            PaymentAmount = x.PaymentAmount,
            PaymentDate = x.PaymentDate,
            PaymentType = x.PaymentType,
            Reason = x.Reason,
            Override = x.Override,
            CreatedByUserId = x.CreatorUserId,
            LendingNumber = x.Lending.LendingNumber,
            IsApproved = x.IsApproved
        });
    }

    public async Task<Payment> AddPayment(CreatePaymentDTM model)
    {
        try
        {
            var activeLoan = _lendingRepository.Entity.OrderByDescending(c => c.Id).FirstOrDefault(x => x.CustomerId == model.CustomerId);

            if (activeLoan == null)
            {
                throw new Exception("No active loan found for this customer");
            }

            activeLoan.IsActive = true;
            activeLoan.IsPaid = false;

            var payment = new Payment
            {
                PaymentDate = model.PaymentDate,
                PaymentAmount = model.PaymentAmount,
                CustomerId = model.CustomerId,
                PaymentType = PaymentEnum.PaymentType.Cash,
                Attachment = string.Empty,
                Override = model.Override,
                Reason = model.Reason ?? string.Empty,
                LendingId = activeLoan.Id
            };
            payment = await _repository.AddAsync(payment);

            await _mediator.Send(payment);

            BackgroundJob.Enqueue(() =>
                _smsService.SendPaymentConfirmation(model.CustomerId, payment));

            return payment;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    public async Task UploadFile(UploadFiles? uploadedFile, Payment payment)
    {
        if (uploadedFile is null) return;
        var files = uploadedFile.Stream.ToArray();
        var filePath = Path.Combine("Attachments", payment.CustomerId.ToString());
        payment.Attachment = Path.Combine(filePath, $"{payment.Id}.{uploadedFile.FileInfo.Type}");

        await _repository.UpdateAsync(payment);
        filePath = Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot", filePath);

        if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
        await File.WriteAllBytesAsync(Path.Combine(filePath, $"{payment.Id}.{uploadedFile.FileInfo.Type}"), files);
    }

    public async Task UploadFile(byte[]? uploadedFile, long paymentId)
    {
        if (uploadedFile is null) return;

        var payment = _repository.Entity.FirstOrDefault(c => c.Id == paymentId);

        var filePath = Path.Combine("Attachments", payment.CustomerId.ToString());

        payment.Attachment = Path.Combine(filePath, $"{paymentId}.png");

        await _repository.SaveChangesAsync();

        filePath = Path.Combine("\\\\172.11.1.242\\c$\\inetpub\\sites\\ccc.interworx.app", "wwwroot", filePath);

        if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);

        await File.WriteAllBytesAsync(Path.Combine(filePath, $"{payment.Id}.png"), uploadedFile);
    }

    public async Task DeletePayment(long paymentId)
    {
        var entity = await _repository.Entity.FindAsync(paymentId);

        if (entity is null) return;

        entity.DeleterUserId = await _userService.GetUserId();
        entity.DeletionAt = DateTimeOffset.Now;
        entity.IsDeleted = true;

        await _repository.SaveChangesAsync();
    }

    public async Task ChangePayment(long paymentId,
                                    long lendingId)
    {
        var entity = await _repository.Entity.FindAsync(paymentId);

        if (entity is null) return;

        entity.LendingId = lendingId;

        await _repository.SaveChangesAsync();
    }

    public async Task<object> GetPaymentForApproval(DataManagerRequest dm)
    {
        var entity = await _userRepository
                     .Entity
                     .Select(c => new PaymentForApprovalDto()
                     {
                         CollectorName = c.FullName,
                         TotalAmount = c.Payments.Where(a => !a.IsApproved)
                                        .Sum(a => a.PaymentAmount),
                         PaymentByDate = c.Payments.Where(a => !a.IsApproved)
                                          .Select(p => new
                                          {
                                              PaymentAmount = p.PaymentAmount,
                                              PaymentDate = p.PaymentDate,
                                              PaymentId = p.Id,
                                              CustomerName = p.Customers.FullName,
                                              LendingNumber = p.Lending.LendingNumber

                                          })
                                          .GroupBy(p => ScalarFunctionHelper.Format(p.PaymentDate, "MM-dd-yyyy"))
                                          .Select(p => new PaymentsForApprovalByDateDto()
                                          {
                                              PaymentDate = p.Key,
                                              TotalAmount = p.Sum(pp => pp.PaymentAmount),
                                              Payments = p.Select(pp => new PaymentsForApprovalDto()
                                              {
                                                  PaymentAmount = pp.PaymentAmount,
                                                  PaymentDate = pp.PaymentDate,
                                                  PaymentId = pp.PaymentId,
                                                  CustomerName = pp.CustomerName,
                                                  LendingNumber = pp.LendingNumber
                                              })
                                                          .ToList()
                                          }).ToList()
                     }).Where(c => c.PaymentByDate.Any())
                     .AsNoTracking()
                     .ToDataResult(dm);



        return entity;
    }

    public async Task PaymentApproval(PaymentsForApprovalByDateDto item)
    {
        foreach (var i in item.Payments)
        {
            var payment = _repository.Entity.FirstOrDefault(c => c.Id == i.PaymentId);

            if (payment is null)
            {
                continue;
            }

            payment.IsApproved = true;

            await _repository.SaveChangesAsync();

            BackgroundJob.Enqueue<ISmsService>((c) => c.PaymentConfirmation(payment.CustomerId, payment));

        }
    }
}