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

namespace MvcCoreNacbarAplicacao.Pages
{
    public class RegistrarEmpresa: PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DBNACBARContext DBNacbar;
        //private readonly IEmailSender _emailSender;

        public RegistrarEmpresa(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
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
        public IList<Estado> LstEstado { get; set; }

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

        public async Task<IActionResult> OnGetAsync(int? id, string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            LstEstado = await DBNacbar.Estado.Select(p => new Estado()
            {
                IdEstado = p.IdEstado,
                DsEstado = p.DsEstado + "-" + p.DsUf
            }).OrderBy(p => p.DsEstado).AsNoTracking().ToListAsync();

            LstBairro = await DBNacbar.Bairro.Select(p => new Bairro()
            {
                IdBairro = p.IdBairro,
                DsBairro = p.DsBairro
            }).AsNoTracking().ToListAsync();

            ViewData["id"] = id;

            if (id != null)
            {
                Empresa = await DBNacbar.Empresa.AsNoTracking().FirstOrDefaultAsync(p => p.IdEmpresa == id);

                if (Empresa == null)
                    return NotFound();

                Empresa.DsCep.FormataTexto(NacbarAplicacao.EnumFormato.CEP);
                Empresa.DsTelefone.FormataTexto(NacbarAplicacao.EnumFormato.TELEFONE);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            if (Empresa.IdEmpresa == 0)
            {
                NacbarAplicacao.IntIdEmpresa = Empresa.IdEmpresa = await DBNacbar.Empresa.MaxAsync(p => (System.Int32?)p.IdEmpresa + 1) ?? 1;
                Empresa.DsCep = Empresa.DsCep.RemoveFormato();
                Empresa.DsTelefone = Empresa.DsTelefone.RemoveFormato();
                Empresa.DtCadastro = Empresa.DtAtualizacao = System.DateTime.Now;
                Empresa.BoAtiva = true;
            
                DBNacbar.Empresa.Add(Empresa);

                await DBNacbar.SaveChangesAsync();

                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser {  UserName = Input.UserName, IntIdEmpresa = NacbarAplicacao.IntIdEmpresa };
                    var result = await _userManager.CreateAsync(user, Input.Password);
                    if (result.Succeeded)
                    {
                        //_logger.LogInformation("User created a new account with password.");//var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        //var callbackUrl = Url.EmailConfirmationLink(user.Id.ToString(), code, Request.Scheme);//await _emailSender.SendEmailConfirmationAsync(Input.Email, callbackUrl);

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
            else
            {
                Empresa.DsCep.FormataTexto(NacbarAplicacao.EnumFormato.CEP);
                Empresa.DsTelefone.FormataTexto(NacbarAplicacao.EnumFormato.TELEFONE);
                Empresa.DtAtualizacao = System.DateTime.Now;
            
                DBNacbar.Empresa.Update(Empresa);

                await DBNacbar.SaveChangesAsync();

                return LocalRedirect(Url.GetLocalUrl(returnUrl));
            }

        }
    }
}
