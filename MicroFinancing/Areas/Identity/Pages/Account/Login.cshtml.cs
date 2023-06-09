using MicroFinancing.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MicroFinancing.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LoginModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [BindProperty]
        public string UserName { get; set; }
        [BindProperty]
        public string Password { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
          
            var result = await _signInManager.PasswordSignInAsync(UserName, Password, false, false);
            if (result.Succeeded)
            {
                //await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, HttpContext.User);
              
                return Redirect("/");
            }

            if (!_userManager.Users.Any(x => x.UserName == UserName))
                ModelState.AddModelError(nameof(UserName), $"{UserName} does not exist");
            if (_userManager.Users.Any(x => x.UserName == UserName))
                ModelState.AddModelError(nameof(UserName), $"{nameof(UserName)} and {nameof(Password)} is incorrect");
            return Page();
        }
    }
}
