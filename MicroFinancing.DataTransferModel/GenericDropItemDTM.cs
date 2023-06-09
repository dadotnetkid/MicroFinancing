using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinancing.DataTransferModel
{
    public class GenericDropItemDTM<T>
    {
        public T Value { get; set; }
        public string Text { get; set; }
    }
}
