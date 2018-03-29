using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MvcCoreNacbarAplicacao.DBNacbarEF;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNacbarAplicacao.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly ILogger<LoginModel> _logger;
        private DBNACBARContext DBNACBARContext;

        public LoginModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, DBNACBARContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            //_logger = logger;
            DBNACBARContext = context;
        }

        //private async Task<SignInResult> PasswordAsync(string UserName, string Password, int IdEmpresa, bool IsPersistent, bool lockoutOnFailure)
        //{
        //    _signInManager.UserManager.
        //}

        public class InputModel
        {
            [Required(ErrorMessage = "INSIRA O USUÁRIO"), Display(Name = "USUÁRIO:")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "INSIRA A SENHA"), Display(Name = "SENHA:")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "LEMBRAR USUÁRIO?")]
            public bool RememberMe { get; set; }
        }

        [BindProperty]
        public string StrId { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
                ModelState.AddModelError(string.Empty, ErrorMessage);

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;

            if (_userManager.Users.Where(p => p.IntIdEmpresa == NacbarAplicacao.IntIdEmpresa).Count() == 0)
               return RedirectToPage("Register");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                PasswordHasher<string> passwordHasher = new PasswordHasher<string>();

                ApplicationUser applicationUser = DBNACBARContext.Users.Where(p => p.UserName == Input.UserName && p.IntIdEmpresa == NacbarAplicacao.IntIdEmpresa && passwordHasher.VerifyHashedPassword(Input.UserName, p.PasswordHash, Input.Password) == PasswordVerificationResult.Success ? true : false).FirstOrDefault();

                if (applicationUser == null)
                {
                    ModelState.AddModelError(string.Empty, "Usuário Incorreto.");
                    return Page();
                }
                else
                {
                    var result = await _signInManager.PasswordSignInAsync(applicationUser, Input.Password, false, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        //_logger.LogInformation("User logged in.");
                        return LocalRedirect(Url.GetLocalUrl(returnUrl));
                    }
                    //if (result.RequiresTwoFactor)
                    //{
                    //    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                    //}
                    //if (result.IsLockedOut)
                    //{
                    //    _logger.LogWarning("User account locked out.");
                    //    return RedirectToPage("./Lockout");
                    //}
                    else
                    {
                        ModelState.AddModelError(string.Empty, "FALHA AO ENTRAR");
                        return Page();
                    }
                }
            }
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
