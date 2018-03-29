using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MvcCoreNacbarAplicacao.Pages
{
    public class ContactModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {
            Message = "ENTRE EM CONTATO CONOSCO";
        }
    }
}
