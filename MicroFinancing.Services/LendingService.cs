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
        private readonly IRepository<Lending, long> _repository;
        private readonly IRepository<Customers, long> _customersRepository;
        private readonly ICustomerService _customerService;
        private readonly IUserService _userService;

        public LendingService(IRepository<Lending, long> repository,
            IRepository<Customers, long> customersRepository,
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
            var numberOfDays = (model.DueDate - model.LendingDate)?.Days ?? 0;

            var date = model.DueDate.GetValueOrDefault();
            var dayss = Enumerable
                .Range(0, numberOfDays+1)
                .Select(n => new { date = model.LendingDate.GetValueOrDefault().AddDays(n) });
            var sundays = dayss
                 .Count(c => c.date.DayOfWeek == DayOfWeek.Sunday);

            var interestRate = (numberOfDays * (10.0M / 30.0M));

            var interestValue = model.Amount * (interestRate / 100M);

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
                Interest = interestValue,
                TotalCredit = interestValue + model.Amount + model.ItemAmount,
                InterestRate = interestRate,
                IsDeleted = false,
                IsActive = true,
                IsPaid = false,
                NumberOfDays = numberOfDays,
                PaymentDays = numberOfDays - sundays,
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
