using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinancing.DataTransferModel
{
    public sealed class SecurityDto
    {
        public sealed class LoginModel
        {
            [Required]
            public string UserName { get; set; }
            [Required]
            public string Password{ get; set; }

        }
    }
}
