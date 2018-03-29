using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MvcCoreNacbarAplicacao.DBNacbarEF;
using System.Threading.Tasks;

namespace MvcCoreNacbarAplicacao.Pages
{
    public class UFExcluirModel : PageModel
    {
        public DBNACBARContext DBNacbar;
        public UFExcluirModel(DBNACBARContext context)
        {
            DBNacbar = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Municipio = await DBNacbar.Municipio.FirstOrDefaultAsync(p => p.IdMunicipio == id);

            if (Municipio == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
                return Page();

            DBNacbar.Municipio.Remove(Municipio);
            await DBNacbar.SaveChangesAsync();

            return RedirectToPage("UFLeitura");
        }

        public Municipio Municipio { get; set; }
    }
}