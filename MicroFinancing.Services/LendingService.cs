using MicroFinancing.DataTransferModel;
using MicroFinancing.Entities;
using MicroFinancing.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroFinancing.Interfaces.Services;

namespace MicroFinancing.Services
{
    public sealed class LendingService : ILendingService
    {
        private readonly IRepository<Lending> _repository;
        private readonly IRepository<Customers> _customersRepository;
        private readonly ICustomerService _customerService;
        private readonly IUserService _userService;

        public LendingService(IRepository<Lending> repository,
            IRepository<Customers> customersRepository,
            ICustomerService customerService,
            IUserService userService)
        {
            _repository = repository;
            _customersRepository = customersRepository;
            _customerService = customerService;
            _userService = userService;
        }

        public IQueryable<LendingGridDTM> Get()
        {
            return _repository.Entity.Select(x => new LendingGridDTM
            {
                Amount = x.Amount + x.ItemAmount,
                Category = x.Category,
                CreatedAt = x.CreatedAt,
                CreatedBy = x.CreatedBy,
                CustomerId = x.CustomerId,
                DueDate = x.DueDate,
                Id = x.Id,
                IsDeleted = x.IsDeleted,
                LendingDate = x.LendingDate,
                CustomerName = $"{x.Customers.FirstName} {x.Customers.LastName}",
                Collector = x.CollectorUser.FirstName + " " + x.CollectorUser.LastName
            });


        }

        public async Task AddLending(CreateLendingDTM model)
        {
            await _repository.AddAsync(new Lending()
            {
                Amount = model.Amount,
                ItemAmount = model.ItemAmount,
                Category = model.Category,
                CreatedAt = DateTime.Now,
                CreatedBy = await _userService.GetUserId(),
                CustomerId = model.CustomerId,
                DueDate = model.DueDate.Value,
                LendingDate = model.LendingDate.Value,
                Collector = model.Collector ?? string.Empty,
                IsDeleted = false,
                IsActive = true,
                IsPaid = false,
            });
        }

        public IQueryable<LendingSummaryGridDTM> GetSummary()
        {
            return _customersRepository.Entity.Select(x => new LendingSummaryGridDTM()
            {
                Id = x.Id,
                CustomerName = x.FirstName + " " + x.LastName,
                TotalBalance = x.Lending.Sum(l => l.Amount + l.ItemAmount) - x.Payments.Sum(p => p.PaymentAmount),
                DueDate = x.Lending.Max(x => x.DueDate),
            });

        }
    }
}
