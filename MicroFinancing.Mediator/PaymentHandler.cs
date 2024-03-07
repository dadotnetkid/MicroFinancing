using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroFinancing.Entities;
using MicroFinancing.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MicroFinancing.Mediator
{
    public class PaymentHandler : AsyncRequestHandler<Payment>
    {
        private readonly IRepository<Payment, long> _repository;
        private readonly IRepository<Lending, long> _lendingRepository;

        public PaymentHandler(IRepository<Payment, long> repository,
            IRepository<Lending, long> lendingRepository)
        {
            _repository = repository;
            _lendingRepository = lendingRepository;
        }

        protected override async Task Handle(Payment request, CancellationToken cancellationToken)
        {
            var getTotalPayment = _repository.Entity.AsNoTracking().Where(x => x.CustomerId == request.CustomerId).Sum(x => x.PaymentAmount);
            var getActiveLoanWithBalance = _lendingRepository.Entity
                .FirstOrDefault(x => x.Id == request.LendingId);
            if (getTotalPayment >= getActiveLoanWithBalance?.Amount)
            {
                getActiveLoanWithBalance.IsActive = false;
                getActiveLoanWithBalance.IsPaid = true;
                await _lendingRepository.UpdateAsync(getActiveLoanWithBalance);
            }
        }
    }
}
