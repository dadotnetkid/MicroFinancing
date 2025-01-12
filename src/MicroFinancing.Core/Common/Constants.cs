using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinancing.Core.Common
{
    public class ConstantVariables
    {
        public const string IncludedProperties = nameof(IncludedProperties);
    }

    public class GenericListItem
    {
    }
    public  enum GenericDropdownItem
    {
        Edit,
        [Description("View Details")]
        ViewDetails,
        Delete,
        [Description("Print Report")]
        PrintReport,
        [Description("Reset Password")]
        ResetPassword,
        AddUser,
        [Description("Set Active Loan")]
        SetActiveLoan,
        [Description("Preview SOA")]
        PreviewSOA,
        [Description("Mark As Paid")]
        MarkAsPaid,
    }

   
}
