using MicroFinancing.Entities;
using MicroFinancing.Interfaces.Repositories;

using Microsoft.EntityFrameworkCore;

public class BuilderFix
{
    public static async Task Run(IServiceProvider services)
    {
        var _repository = services.CreateScope().ServiceProvider.GetService<IRepository<Customers, long>>();
        var lendingRepo = services.CreateScope().ServiceProvider.GetService<IRepository<Lending, long>>();
        var paymentRepo = services.CreateScope().ServiceProvider.GetService<IRepository<Payment, long>>();

        await _repository.Database.BeginTransactionAsync();

        var customersList = _repository.Entity
                                       .Where(c => c.Lending.Any())
                                       .Where(c => !c.IsDeleted).ToList();

        try
        {
            foreach (var customer in customersList)
            {
                var name = customer.FullName;
                var id = customer.Id;

                var lending = await lendingRepo.Entity.Where(c => c.CustomerId == customer.Id)
                                               .Where(c => !c.IsDeleted)
                                               .Where(c => !c.IsPaid)
                                               .Where(c => c.IsActive)
                                               .OrderBy(c => c.Id)
                                               .LastOrDefaultAsync();

                if (lending == null)
                {
                    continue;
                }


                var dueDate = lending.DueDate;
                var lendingDate = lending.LendingDate;

                var payments = await paymentRepo.Entity
                                                .Where(c => c.CustomerId == customer.Id)
                                                .Where(c => !c.IsDeleted)
                                                .Where(c => c.PaymentDate > lendingDate)
                                                .Where(c => c.PaymentDate <= dueDate)
                                                .Where(c => c.LendingId != lending.Id)
                                                .ToListAsync();

                if (!payments.Any())
                {
                    continue;
                }

                foreach (var payment in payments)
                {
                    payment.LendingId = lending.Id;
                }

                await paymentRepo.SaveChangesAsync();
            }
        }
        catch (Exception e)
        {
            await _repository.Database.RollbackTransactionAsync();

            return;
        }


        await _repository.Database.CommitTransactionAsync();
    }

    private static List<long> ListOfIds = new();
    private static IRepository<Lending, long>? repository;

    private static void GetLatestLoan(long id = 0)
    {
        var qry = repository.Entity.Where(c => c.ParentLendingId != 0);

        if (id > 0)
        {
            qry = qry.Where(c => c.ParentLendingId == id);
        }

        var res = qry.ToList();

        foreach (var _res in res)
        {
            var ___ = new Lending();

            if (id == 0)
            {
                ___ = repository.Entity.Where(c => c.Id == _res.ParentLendingId)
                                .FirstOrDefault();
            }
            else
            {
                ___ = repository.Entity.Where(c => c.ParentLendingId == _res.Id)
                                .FirstOrDefault();
            }

            if (___.IsRestruct)
            {
                GetLatestLoan(___.Id);
                return;
            }
            else
            {
                _res.IsPaid = false;
                _res.IsActive = true;

            }
        }
    }
}
