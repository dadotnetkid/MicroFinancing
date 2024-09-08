using MicroFinancing.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinancing.Core.Enumeration
{
    public class ItemsEnumeration
    {
        public enum ItemType
        {

        }
    }
    public class LendingEnumeration
    {
        public enum LendingCategory
        {
            Cash = 1,
            Item = 2,
            Both
        }

        public enum Duration
        {
            [Description("40 Days")]
            [DefaultValue(40)]
            FortyDays = 0,
            [Description("36 Days")]
            [DefaultValue(36)]
            ThirtySixDays = 2,
            [Description("Custom")]
            Custom = 1,
        }
    }

    public enum CustomerFlag
    {

        [Description("Good Payer")]
        [Color("success")]
        GoodPayer = 0,
        [Description("Late Payer")]
        [Color("warning")]
        LatePayer = 1,
        [Description("Not Paying")]
        [Color("danger")]
        NotPaying = 2,
    }
}
