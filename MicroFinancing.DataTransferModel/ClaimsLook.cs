using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroFinancing.Core.Common;

namespace MicroFinancing.DataTransferModel
{
    public sealed class ClaimsValueModel
    {
        public List<ClaimsLookup> ClaimsValueModels => Initialize();


        private List<ClaimsLookup> Initialize()
        {
            return new List<ClaimsLookup>
            {
                new()
                {
                    Name = ClaimsConstant.Administrator,
                    ClaimType = ClaimsConstant.Administrator,
                    Value = ClaimsConstant.Administrator,
                    Description="Administrator"
                },
                new()
                {
                    Name = ClaimsConstant.Users.View,
                    ClaimType = ClaimsConstant.Users.ClaimType,
                    Value = ClaimsConstant.Users.View,
                    Description="View User"
                },
                new()
                {
                    Name = ClaimsConstant.Users.Manage,
                    ClaimType = ClaimsConstant.Users.ClaimType,
                    Value = ClaimsConstant.Users.Manage,
                    Description="Manage User"
                },
                new()
                {
                    Name = ClaimsConstant.Roles.View,
                    ClaimType = ClaimsConstant.Roles.ClaimType,
                    Value = ClaimsConstant.Roles.View,
                    Description="View Roles"
                },
                new()
                {
                    Name = ClaimsConstant.Roles.Manage,
                    ClaimType = ClaimsConstant.Roles.ClaimType,
                    Value = ClaimsConstant.Roles.Manage,
                    Description="Manage Roles"
                },
                new()
                {
                    Name = ClaimsConstant.Customer.Manage,
                    ClaimType = ClaimsConstant.Customer.ClaimType,
                    Value = ClaimsConstant.Customer.Manage,
                    Description="Manage Customer"
                },
                new()
                {
                    Name = ClaimsConstant.Customer.View,
                    ClaimType = ClaimsConstant.Customer.ClaimType,
                    Value = ClaimsConstant.Customer.View,
                    Description="View Customer"
                },
                new()
                {
                    Name = ClaimsConstant.Customer.Print,
                    ClaimType = ClaimsConstant.Customer.ClaimType,
                    Value = ClaimsConstant.Customer.Print,
                    Description="Print Customer"
                },
                new()
                {
                    Name = ClaimsConstant.Customer.SetFlag,
                    ClaimType = ClaimsConstant.Customer.ClaimType,
                    Value = ClaimsConstant.Customer.SetFlag,
                    Description="Set Flag Customer"
                },
                new()
                {
                    Name = ClaimsConstant.Customer.ManageLoan,
                    ClaimType = ClaimsConstant.Customer.ClaimType,
                    Value = ClaimsConstant.Customer.ManageLoan,
                    Description="Manage Loan Customer"
                },
                new()
                {
                    Name = ClaimsConstant.Customer.AddLoan,
                    ClaimType = ClaimsConstant.Customer.ClaimType,
                    Value = ClaimsConstant.Customer.AddLoan,
                    Description="Add Loan Customer"
                },
                new()
                {
                    Name = ClaimsConstant.Customer.ManagePayment,
                    ClaimType = ClaimsConstant.Customer.ClaimType,
                    Value = ClaimsConstant.Customer.ManagePayment,
                    Description="Manage Payment Customer"
                },
                new()
                {
                    Name = ClaimsConstant.Customer.AddPayment,
                    ClaimType = ClaimsConstant.Customer.ClaimType,
                    Value = ClaimsConstant.Customer.AddPayment,
                    Description="Add Payment Customer"
                },
                new()
                {
                    Name = ClaimsConstant.Customer.Add,
                    ClaimType = ClaimsConstant.Customer.ClaimType,
                    Value = ClaimsConstant.Customer.Add,
                    Description="Add Customer"
                },
                new()
                {
                    Name = ClaimsConstant.Customer.ViewAllCustomer,
                    ClaimType = ClaimsConstant.Customer.ClaimType,
                    Value = ClaimsConstant.Customer.ViewAllCustomer,
                    Description=ClaimsConstant.Customer.ViewAllCustomer
                },
                new()
                {
                    Name = ClaimsConstant.Reports.ViewCollectionSummaryReport,
                    ClaimType = ClaimsConstant.Reports.ClaimType,
                    Value = ClaimsConstant.Reports.ViewCollectionSummaryReport,
                    Description=ClaimsConstant.Reports.ViewCollectionSummaryReport
                },
                new()
                {
                    Name = ClaimsConstant.Reports.ViewCollectorClientReport,
                    ClaimType = ClaimsConstant.Reports.ClaimType,
                    Value = ClaimsConstant.Reports.ViewCollectorClientReport,
                    Description=ClaimsConstant.Reports.ViewCollectorClientReport
                },
                new()
                {
                    Name = ClaimsConstant.Customer.Edit,
                    ClaimType = ClaimsConstant.Customer.ClaimType,
                    Value = ClaimsConstant.Customer.Edit,
                    Description=ClaimsConstant.Customer.Edit
                },
                new()
                {
                    Name = ClaimsConstant.Customer.Delete,
                    ClaimType = ClaimsConstant.Customer.ClaimType,
                    Value = ClaimsConstant.Customer.Delete,
                    Description=ClaimsConstant.Customer.Delete
                }
            };
        }



    }

    public class ClaimsLookup
    {
        public string Description { get; set; }

        public string Value { get; set; }

        public string ClaimType { get; set; }

        public string Name { get; set; }
    }
}
