using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using MicroFinancing.Core.Enumeration;
using MicroFinancing.Interfaces.Services;
using MicroFinancing.Services.Handlers.PaymentCommands;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Syncfusion.Blazor;

namespace MicroFinancing.Services
{
    public sealed class LendingService : ILendingService
    {
        private readonly IRepository<Lending, long> _repository;
        private readonly IRepository<Customers, long> _customersRepository;
        private readonly IUserService _userService;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IRepository<LendingForApproval, long> _lendingForApprovalRepository;

        public LendingService(IRepository<Lending, long> repository,
                              IRepository<Customers, long> customersRepository,
                              IUserService userService,
                              IServiceScopeFactory scopeFactory,
                              IMediator mediator,
                              IMapper mapper,
                              IRepository<LendingForApproval, long> lendingForApprovalRepository)
        {
            _repository = repository;
            _customersRepository = customersRepository;
            _userService = userService;
            _scopeFactory = scopeFactory;
            _mediator = mediator;
            _mapper = mapper;
            _lendingForApprovalRepository = lendingForApprovalRepository;
        }

        public IQueryable<LendingGridDTM> Get()
        {
            return _repository.Entity.Select(x => new LendingGridDTM
            {
                Amount = x.Amount + x.ItemAmount,
                TotalCredit = x.TotalCredit,
                Category = x.Category,
                CreatedAt = x.CreatedAt,
                CreatedBy = x.CreatedBy,
                CustomerId = x.CustomerId,
                DueDate = x.DueDate,
                Id = x.Id,
                IsDeleted = x.IsDeleted,
                LendingDate = x.LendingDate,
                CustomerName = $"{x.Customers.FirstName} {x.Customers.LastName}",
                Collector = x.CollectorUser.FirstName + " " + x.CollectorUser.LastName,
                IsRestruct = x.IsRestruct,
                IsPaid = x.IsPaid,
                ParentId = x.ParentLendingId,
                IsActive = x.IsActive,
                LendingNumber = x.LendingNumber

            });

        }

        public async Task AddLending(CreateLendingDTM model)
        {
            var numberOfDays = CalculateInterest(model, out var sundays, out var interestRate, out var interestValue);

            await _repository.AddAsync(new Lending()
            {
                Amount = model.Amount,
                ItemAmount = model.ItemAmount,
                Category = model.Category,
                CreatedAt = DateTime.Now,
                CreatedBy = await _userService.GetUserId(),
                CustomerId = model.CustomerId,
                DueDate = model.DueDate.Value,
                LendingDate = Convert.ToDateTime(model.LendingDate.Value.ToShortDateString()),
                Collector = model.Collector ?? string.Empty,
                Interest = interestValue,
                TotalCredit = GetTotalCredit(model, interestValue) ?? 0M,
                InterestRate = interestRate,
                IsDeleted = false,
                IsActive = true,
                IsPaid = false,
                DsTax = GetDsTax(model),
                NumberOfDays = numberOfDays,
                PaymentDays = model.Duration == LendingEnumeration.Duration.FortyDays ? 36 : (numberOfDays - sundays),
                Duration = model.Duration
            });
        }

        private decimal? GetDsTax(BaseLendingDTM model)
        {
            return ((model.Amount + model.ItemAmount)) * 0.00125M;
        }

        private decimal? GetTotalCredit(BaseLendingDTM model,
                                       decimal interestValue)
        {
            var subTotal = model.Amount + model.ItemAmount;

            var dsTax = GetDsTax(model);

            return interestValue + subTotal + dsTax;
        }

        public async Task<object> GetSummary(DataManagerRequest dm,
                                             string userId)
        {

            var query = _customersRepository.Entity.AsQueryable();

            if (!string.IsNullOrEmpty(userId))
            {
                query = query.Where(x => x.Lending.Any(l => l.Collector == userId));
            }

            var result = await query.Select(x => new LendingSummaryGridDTM()
            {
                Id = x.Id,
                CustomerName = x.FirstName + " " + x.LastName,
                TotalBalance = x.Lending.Where(c => c.IsActive).Sum(l => l.TotalCredit) - x.Payments
                                                                                                   .Where(c => c.Lending.IsActive)
                                                                                                   .Sum(p => p.PaymentAmount),
                DueDate = x.Lending.Max(x => x.DueDate),
            }).ToDataResult(dm);

            return result;
        }

        public async Task DeleteLending(long id)
        {
            var entity = await _repository.Entity.FindAsync(id);

            if (entity == null)
            {
                return;
            }

            entity.IsDeleted = true;
            entity.DeletionAt = DateTime.Now;

            await _repository.SaveChangesAsync();
        }

        public async Task EditLending(EditLendingDTM model)
        {
            var res = _repository.Entity.FirstOrDefault(x => x.Id == model.Id);

            if (res == null)
            {
                throw new Exception("Lending not found");
            }

            var numberOfDays = CalculateInterest(model, out var sundays, out var interestRate, out var interestValue);

            res.Amount = model.Amount;
            res.Category = model.Category;
            res.Collector = model.Collector;
            res.DueDate = model.DueDate.GetValueOrDefault();
            res.ItemAmount = model.ItemAmount;
            res.LendingDate = model.LendingDate.GetValueOrDefault();
            res.Interest = interestValue;
            res.TotalCredit = GetTotalCredit(model, interestValue) ?? 0;
            res.NumberOfDays = numberOfDays;
            res.InterestRate = interestRate;
            res.PaymentDays = model.Duration == LendingEnumeration.Duration.FortyDays ? 36 : (numberOfDays - sundays);
            res.Duration = model.Duration;
            res.UpdateAt = DateTimeOffset.Now;

            await _repository.SaveChangesAsync();
        }

        private int CalculateInterest(BaseLendingDTM model,
                                             out int sundays,
                                             out decimal interestRate,
                                             out decimal interestValue)
        {
            var numberOfDays = model.Duration == LendingEnumeration.Duration.Custom ? ((model.DueDate - model.LendingDate)?.Days ?? 0) : model.Duration.GetDefault<int>();

            var dayss = Enumerable
                        .Range(0, numberOfDays + 1)
                        .Select(n => new { date = model.LendingDate.GetValueOrDefault().AddDays(n) });
            sundays = dayss
                .Count(c => c.date.DayOfWeek == DayOfWeek.Sunday);

            interestRate = model.Duration.GetInterest() ?? Math.Round((numberOfDays * 0.003325M) * 100, 2);

            interestValue = model.Amount * (interestRate / 100);

            model.DueDate = model.LendingDate.GetValueOrDefault().AddDays(numberOfDays);

            return numberOfDays;
        }

        public EditLendingDTM GetLendingDetailsForEdit(long id)
        {
            var res = _repository.Entity.Where(x => x.Id == id).Select(x => new EditLendingDTM
            {
                Amount = x.Amount,
                Category = x.Category,
                Collector = x.Collector,
                CustomerId = x.CustomerId,
                DueDate = x.DueDate,
                Id = x.Id,
                ItemAmount = x.ItemAmount,
                LendingDate = x.LendingDate,
            }).FirstOrDefault();

            return res;
        }

        public async Task SetActiveLoan(long id)
        {
            var res = await _repository.Entity.FirstOrDefaultAsync(x => x.Id == id);

            foreach (var i in _repository.Entity.Where(c => c.CustomerId == res.CustomerId && c.Id != id))
            {
                i.IsPaid = true;
                i.IsActive = false;
            }

            res.IsActive = true;
            res.IsPaid = false;
            res.IsRestruct = false;

            await _repository.SaveChangesAsync();
        }

        public async Task MarkAsPaid(long lendingId, string creatorUserId)
        {
            var lending = await _repository.Entity.FirstOrDefaultAsync(x => x.Id == lendingId);

            await _mediator.Send(new MarkAsPaidCommand()
            {
                LendingId = lending.Id,
                CustomerId = lending.CustomerId,
                CreatorId = creatorUserId,
                TotalCredit = lending.TotalCredit
            });
        }

        public async Task<List<LendingForApprovalGridDTM>> GetLendingNotApproved()
        {
            var res = _lendingForApprovalRepository.Entity
                                                   .Where(c => !c.IsApproved);

            var dto = _mapper.ProjectTo<LendingForApprovalGridDTM>(res);

            return await dto.ToListAsync();
        }

        public async Task AddLendingForApproval(CreateLendingForApprovalDTM model)
        {
            var entity = _mapper.Map<LendingForApproval>(model);

            var numberOfDays = CalculateInterest(model, out var sundays, out var interestRate, out var interestValue);

            entity.CreatedAt = DateTime.Now;
            entity.CreatorUserId = await _userService.GetUserId();
            entity.CreatedBy = await _userService.GetUserId();
            entity.NumberOfDays = numberOfDays;
            entity.PaymentDays = model.Duration == LendingEnumeration.Duration.FortyDays ? 36 : (numberOfDays - sundays);
            entity.InterestRate = interestRate;
            entity.Interest = interestValue;
            entity.TotalCredit = GetTotalCredit(model, interestValue) ?? 0;

            await _lendingForApprovalRepository.AddAsync(entity);
        }

        public async Task Release(LendingForApprovalGridDTM? item)
        {
            var lendingForApproval = await _lendingForApprovalRepository.Entity.FirstOrDefaultAsync(c => c.Id == item.Id);

            var res = _mapper.Map<Lending>(lendingForApproval);

            res.Id = 0;

            await _repository.AddAsync(res);

            lendingForApproval.IsApproved = true;

            await _lendingForApprovalRepository.SaveChangesAsync();


        }
    }
}
