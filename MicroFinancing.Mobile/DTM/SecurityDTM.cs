using System.ComponentModel.DataAnnotations;

namespace MicroFinancing.Mobile.DTM
{
    public sealed class SecurityDTM
    {
        public sealed class LoginModel
        {
            [Required]
            public string UserName { get; set; }
            [Required]
            public string Password { get; set; }

        }
    }

}