using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinancing.Core.Common
{
    public static class ConverterHelper
    {
        public static T ToTypeOf<T>(this object obj) where T : IConvertible
        {
            if (typeof(T).IsEnum)
            {
                return (T)Enum.Parse(typeof(T), obj.ToString(), true);
            }
            return (T)Convert.ChangeType(obj, typeof(T));
        }
    }
}
