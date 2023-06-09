using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinancing.Core.Attributes
{
    public class ColorAttribute : Attribute
    {
        public string Color { get; private set; }

        public ColorAttribute(string Color)
        {
            this.Color = Color;
        }
    }
}
