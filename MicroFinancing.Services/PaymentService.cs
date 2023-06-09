using MicroFinancing.DataTransferModel;
using MicroFinancing.Entities;
using MicroFinancing.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MicroFinancing.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor.Inputs;
using Microsoft.Extensions.Hosting;

namespace MicroFinancing.Services
{
    public sealed class PaymentService : IPaymentService
    {
        private readonly IRepository<Payment> _repository;
        private readonly IRepository<Lending> _lendingRepository;
        private readonly IHostEnvironment _hostEnvironment;
        private readonly IUserService _userService;
        private readonly IMediator _mediator;

        public PaymentService(IRepository<Payment> repository,
            IRepository<Lending> lendingRepository,
            IHostEnvironment hostEnvironment, IUserService userService, IMediator mediator)
        {
            _repository = repository;
            _lendingRepository = lendingRepository;
            _hostEnvironment = hostEnvironment;
            _userService = userService;
            _mediator = mediator;
        }

        public IQueryable<PaymentGridDTM> Get()
        {
            return _repository.Entity.Select(x => new PaymentGridDTM()
            {
                CreatedAt = x.CreatedAt,
                CreatedBy = x.CreatedByUser.FirstName + " " + x.CreatedByUser.LastName,
                CustomerName=x.Customers.FullName,
                CustomerId = x.CustomerId,
                Id = x.Id,
                PaymentAmount = x.PaymentAmount,
                PaymentDate = x.PaymentDate,
                PaymentType = x.PaymentType,
                Reason = x.Reason,
                Override = x.Override,
                CreatedByUserId = x.CreatedBy
            });
        }

        public async Task<Payment> AddPayment(CreatePaymentDTM model)
        {
            try
            {
                var activeLoan = _lendingRepository.Entity.Where(x => x.CustomerId == model.CustomerId).Max(x => x.Id);
                var payment = new Payment()
                {
                    CreatedBy = await _userService.GetUserId(),
                    PaymentDate = model.PaymentDate,
                    CreatedAt = DateTime.UtcNow,
                    PaymentAmount = model.PaymentAmount,
                    CustomerId = model.CustomerId,
                    PaymentType = Core.Enumeration.PaymentEnum.PaymentType.Cash,
                    Attachment = string.Empty,
                    Override = model.Override,
                    Reason = model.Reason ?? string.Empty,
                    LendingId = activeLoan,
                };
                payment = await _repository.AddAsync(payment);
                await _mediator.Send(payment);
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
    }
}