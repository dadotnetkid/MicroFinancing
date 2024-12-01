using MicroFinancing.Core.Enumeration;
using MicroFinancing.Interfaces.Services;

using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor.RichTextEditor;

namespace MicroFinancing.Services.Handlers;

public class ReConstructHandler
{
    private readonly IRepository<Lending, long> _repository;
    private readonly ISmsService _smsService;

    public ReConstructHandler(IRepository<Lending, long> repository,
                              ISmsService smsService)
    {
        _repository = repository;
        _smsService = smsService;
    }
    public async Task Handle()
    {
        var transaction = await _repository.Database.BeginTransactionAsync();
        try
        {
            var query = await _repository.Entity
                                         .Where(c => !c.IsPaid)
                                         .Where(c => c.DueDate < DateTime.Now)
                                         .Select(c => new RestructureCustomerDTM
                                         {
                                             Balance = c.TotalCredit - c.Payments.Sum(p => p.PaymentAmount),
                                             Id = c.Id,
                                             CustomerId = c.CustomerId,
                                             Amount = c.Amount,
                                             ItemAmount = c.ItemAmount,
                                             Category = c.Category,
                                             DueDate = c.DueDate,
                                             LendingDate = c.LendingDate,
                                             Collector = c.Collector,
                                             PhoneNumber = c.Customers.PhoneNumber,
                                             FullName = c.Customers.FullName
                                         })
                                         .AsNoTracking()
                                         .ToListAsync();

            foreach (var item in query)
            {
                await Restruct(item);

                //  BackgroundJob.Enqueue(() => _smsService.SendRestructureToCustomer(item.PhoneNumber, item.FullName));
            }

            /*BackgroundJob.Enqueue(() => _smsService.SendRestructureToAdmin(string.Join("\r\n",
                                                                                       query.Select(c => c.FullName)
                                                                                            .ToList())));*/

            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
        }

    }

    public async Task Restruct(long customerId)
    {
        var query = await _repository.Entity
                               .Where(c => !c.IsPaid)
                               .Where(c => c.DueDate < DateTime.Now)
                               .Select(c => new RestructureCustomerDTM
                               {
                                   Balance = c.TotalCredit - c.Payments.Sum(p => p.PaymentAmount),
                                   Id = c.Id,
                                   CustomerId = c.CustomerId,
                                   Amount = c.Amount,
                                   ItemAmount = c.ItemAmount,
                                   Category = c.Category,
                                   DueDate = c.DueDate,
                                   LendingDate = c.LendingDate,
                                   Collector = c.Collector,
                                   PhoneNumber = c.Customers.PhoneNumber,
                                   FullName = c.Customers.FullName
                               })
                               .AsNoTracking()
                               .FirstOrDefaultAsync();

        await Restruct(query);
    }

    private async Task Restruct(RestructureCustomerDTM item)
    {
        try
        {
            var lending = _repository.Entity
                                     .Include(c => c.Payments)
                                     .FirstOrDefault(c => c.Id == item.Id);

            if (lending == null)
            {
                return;
            }

            lending.IsRestruct = true;
            lending.IsPaid = true;
            lending.IsActive = false;
            lending.UpdateAt = DateTimeOffset.Now;

            lending.Payments.Add(new Payment()
            {
                PaymentAmount = item.Balance,
                PaymentDate = DateTime.Now,
                PaymentType = PaymentEnum.PaymentType.System,
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                Attachment = "",
                Reason = "System Generated Payment",
                CreatorUserId = "8BF43507-01E3-4427-A74E-9E5024E7144E",
                CustomerId = item.CustomerId
            });

            var numberOfDays = 30;

            var dayss = Enumerable
                        .Range(0, numberOfDays + 1)
                        .Select(n => new { date = item.DueDate.AddDays(n) });

            var sundays = dayss
                .Count(c => c.date.DayOfWeek == DayOfWeek.Sunday);

            var interestRate = 10;

            var interestValue = item.Balance * (interestRate / 100M);

            var dueDate = item.DueDate.AddDays(numberOfDays);

            await _repository.Entity.AddAsync(new Lending()
            {
                Amount = item.Balance ?? 0,
                ItemAmount = item.ItemAmount,
                Category = item.Category,
                CreatedAt = DateTime.Now,
                CreatedBy = "8BF43507-01E3-4427-A74E-9E5024E7144E",
                CreatorUserId = "8BF43507-01E3-4427-A74E-9E5024E7144E",
                CustomerId = item.CustomerId,
                DueDate = dueDate,
                LendingDate = item.DueDate,
                Collector = item.Collector ?? string.Empty,
                Interest = interestValue.GetValueOrDefault(),
                TotalCredit = interestValue.GetValueOrDefault() + item.Balance.GetValueOrDefault(),
                InterestRate = interestRate,
                IsDeleted = false,
                IsActive = true,
                IsPaid = false,
                NumberOfDays = numberOfDays,
                PaymentDays = (numberOfDays - sundays),
                Duration = LendingEnumeration.Duration.ThirtyDays,
                ParentLendingId = lending.Id
            });

            await _repository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            throw;
        }
    }
}
