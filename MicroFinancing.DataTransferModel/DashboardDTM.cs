using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinancing.DataTransferModel
{
    public sealed class DashboardDTM
    {
        public int TotalCustomer { get; set; }
        public decimal? TotalLoans { get; set; }
        public decimal? TotalAmountPaid { get; set; }
        public decimal TotalBalance => (TotalLoans ?? 0) - (TotalAmountPaid ?? 0);
    }
}
