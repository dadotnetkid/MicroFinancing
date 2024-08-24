using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinancing.Core.Common
{
    public static class ClaimsConstant
    {
        public const string Administrator = "Administrator";
        public const string ClaimType = "Permission";
        public static class Users
        {
            public const string Manage = "Manage User";
            public const string View = "View User";
            public const string ClaimType = "Permission";
        }

        public static class Roles
        {
            public const string Manage = "Manage Role";
            public const string View = "View Role";
            public const string ClaimType = "Permission";
        }

        public static class Customer
        {
            public const string Manage = "Manage Customer";
            public const string Print = "Print SOA";
            public const string AddLoan = "Add Loan";
            public const string View = "View Customer";
            public const string ManageLoan = "Manage Loan";
            public const string ManagePayment = "Manage Payment";
            public const string OverridePayment = "Override Payment";
            public const string AddPayment = "Add Payment";
            public const string SetFlag = "Set Flag";
            public const string ClaimType = "Permission";
    
        }

        public static class Policy
        {
            public static class Customer
            {
                public static string[] Manage => new[] { ClaimsConstant.Customer.Manage, Administrator };
                public static string[] ManageLoan => new[] { ClaimsConstant.Customer.ManageLoan, ClaimsConstant.Customer.Manage, Administrator };
                public static string[] ManagePayment => new[] { ClaimsConstant.Customer.ManagePayment, ClaimsConstant.Customer.Manage, Administrator };
                public static string[] OverridePayment => new[] { ClaimsConstant.Customer.OverridePayment, ClaimsConstant.Customer.ManagePayment, ClaimsConstant.Customer.Manage, Administrator };

                public static string[] Print => new[] { ClaimsConstant.Customer.Print, ClaimsConstant.Customer.Manage, Administrator };
                public static string[] View => new[] { ClaimsConstant.Customer.View, ClaimsConstant.Customer.Manage, Administrator };
                public static string[] SetFlag => new[] { ClaimsConstant.Customer.SetFlag, ClaimsConstant.Customer.Manage, Administrator };
                public static string[] AddLoan => new[] { ClaimsConstant.Customer.AddLoan, ClaimsConstant.Customer.Manage, ClaimsConstant.Customer.ManageLoan, Administrator };
                public static string[] AddPayment => new[] { ClaimsConstant.Customer.AddPayment,ClaimsConstant.Customer.ManagePayment, ClaimsConstant.Customer.Manage,  Administrator };
            }
            public static class Users
            {
                public static string[] Manage => new[] { ClaimsConstant.Users.Manage, Administrator };
                public static string[] View => new[] { ClaimsConstant.Users.View, ClaimsConstant.Users.Manage, Administrator };
            }

            public static class Roles
            {
                public static string[] Manage => new[] { ClaimsConstant.Roles.Manage, Administrator };
                public static string[] View => new[] { ClaimsConstant.Roles.View, ClaimsConstant.Roles.Manage, Administrator };
            }
        }

    }
}
