using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcCoreNacbarAplicacao.DBNacbarEF;
using MvcCoreNacbarAplicacao.Services;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MvcCoreNacbarAplicacao.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"USU�RIO COM ID DESABILITADO: '{_userManager.GetUserId(User)}'.");
            }

            Username = user.UserName;

            //Input = new InputModel
            //{
            //    Email = user.Email,
            //    PhoneNumber = user.PhoneNumber
            //};
            //
            //IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Usu�rio '{_userManager.GetUserName(User)}' desabilitado.");
            }

            var setUserResult = await _userManager.SetUserNameAsync(user, Username);
            if (!setUserResult.Succeeded)
                throw new ApplicationException($"Usu�rio '{user.UserName}'.");
            else
                await _signInManager.RefreshSignInAsync(user);

            //if (Input.Email != user.Email)
            //{
            //    var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
            //    if (!setEmailResult.Succeeded)
            //    {
            //        throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
            //    }
            //}
            //
            //if (Input.PhoneNumber != user.PhoneNumber)
            //{
            //    var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
            //    if (!setPhoneResult.Succeeded)
            //    {
            //        throw new ApplicationException($"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
            //    }
            //}

            StatusMessage = "USU�RIO ATUALIZADO.";
            return RedirectToPage();
        }

        [Display(Name = "USU�RIO")]
        [BindProperty]
        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        //[BindProperty]
        //public InputModel Input { get; set; }
        //
        //public class InputModel
        //{
        //    [Required]
        //    [EmailAddress]
        //    public string Email { get; set; }
        //
        //    [Phone]
        //    [Display(Name = "N�mero de Telefone")]
        //    public string PhoneNumber { get; set; }
        //}

        //public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }
        //
        //    var user = await _userManager.GetUserAsync(User);
        //    if (user == null)
        //    {
        //        throw new ApplicationException($"Usu�rio com ID desabilitado: '{_userManager.GetUserId(User)}'.");
        //    }
        //
        //    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //    //var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
        //    //await _emailSender.SendEmailConfirmationAsync(user.Email, callbackUrl);
        //
        //    //StatusMessage = "Verification email sent. Please check your email.";
        //    return RedirectToPage();
        //}
    }
}
