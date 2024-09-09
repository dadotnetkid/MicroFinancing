using Hangfire;
using MediatR;
using MicroFinancing.Core.Enumeration;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Entities;
using MicroFinancing.Interfaces.Repositories;
using MicroFinancing.Interfaces.Services;
using Microsoft.Extensions.Hosting;
using Syncfusion.Blazor.Inputs;

namespace MicroFinancing.Services;

public sealed class PaymentService : IPaymentService
{
    private readonly IHostEnvironment _hostEnvironment;
    private readonly IRepository<Lending, long> _lendingRepository;
    private readonly IMediator _mediator;
    private readonly ISmsService _smsService;
    private readonly IRepository<Payment, long> _repository;
    private readonly IUserService _userService;

    public PaymentService(IRepository<Payment, long> repository,
        IRepository<Lending, long> lendingRepository,
        IHostEnvironment hostEnvironment,
        IUserService userService,
        IMediator mediator,
        ISmsService smsService)
    {
        _repository = repository;
        _lendingRepository = lendingRepository;
        _hostEnvironment = hostEnvironment;
        _userService = userService;
        _mediator = mediator;
        _smsService = smsService;
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
            CreatedByUserId = x.CreatorUserId
        });
    }

    public async Task<Payment> AddPayment(CreatePaymentDTM model)
    {
        try
        {
            var activeLoan = _lendingRepository.Entity.Where(x => x.CustomerId == model.CustomerId).Max(x => x.Id);

            var payment = new Payment
            {
                PaymentDate = model.PaymentDate,
                PaymentAmount = model.PaymentAmount,
                CustomerId = model.CustomerId,
                PaymentType = PaymentEnum.PaymentType.Cash,
                Attachment = string.Empty,
                Override = model.Override,
                Reason = model.Reason ?? string.Empty,
                LendingId = activeLoan
            };
            payment = await _repository.AddAsync(payment);
            
            await _mediator.Send(payment);

            BackgroundJob.Enqueue(() =>
                _smsService.SendPaymentConfirmation(model.CustomerId,
                    payment.PaymentAmount.GetValueOrDefault().ToString("n2")));

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
}