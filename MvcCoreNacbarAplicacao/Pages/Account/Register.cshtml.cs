using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MvcCoreNacbarAplicacao.DBNacbarEF;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MvcCoreNacbarAplicacao.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly ILogger<LoginModel> _logger;
        private readonly DBNACBARContext DBNacbar;
        //private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            //ILogger<LoginModel> logger,
            DBNACBARContext context)
            //IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            //_logger = logger;
            DBNacbar = context;
            //_emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [BindProperty]
        public Empresa Empresa { get; set; }

        [BindProperty]
        public IList<Municipio> LstMunicipio { get; set; }

        [BindProperty]
        public IList<Bairro> LstBairro { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "INSIRA O USUÁRIO"), Display(Name = "USUÁRIO")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "INSIRA A SENHA"), Display(Name = "SENHA")]
            [StringLength(100, ErrorMessage = @"Senha deve ter no mínimo de 6 caracteres")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "CONFIRMAR SENHA")]
            [Compare("Password", ErrorMessage = @"Senhas não combinam")]
            public string ConfirmPassword { get; set; }
        }

        public IActionResult OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            if (NacbarAplicacao.BolRedirect == true)
                return RedirectToPage("/Index");

            NacbarAplicacao.BolRedirect = true;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = Input.UserName, IntIdEmpresa = NacbarAplicacao.IntIdEmpresa };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    //_logger.LogInformation("User created a new account with password.");

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.EmailConfirmationLink(user.Id.ToString(), code, Request.Scheme);
                    //await _emailSender.SendEmailConfirmationAsync(Input.Email, callbackUrl);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(Url.GetLocalUrl(returnUrl));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
    