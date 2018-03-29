using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MvcCoreNacbarAplicacao.DBNacbarEF;
using System.Threading.Tasks;

namespace MvcCoreNacbarAplicacao.Pages
{
    public class EmpresaExcluirModel : PageModel
    {
        public DBNACBARContext DBNacbar;
        public EmpresaExcluirModel(DBNACBARContext context)
        {
            DBNacbar = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Empresa = await DBNacbar.Empresa.FirstOrDefaultAsync(p => p.IdEmpresa == id);

            if (Empresa == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
                return Page();

            DBNacbar.Empresa.Remove(Empresa);
            await DBNacbar.SaveChangesAsync();

            return RedirectToPage("EmpresaLeitura");
        }

        [BindProperty]
        public Empresa Empresa { get; set; }
    }
}