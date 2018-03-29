using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MvcCoreNacbarAplicacao.DBNacbarEF;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNacbarAplicacao.Pages
{
    public class UFCriarEditarModel : PageModel
    {
        DBNACBARContext DBNacbar;

        public UFCriarEditarModel(DBNACBARContext context)
        {
            DBNacbar = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            LstEstado = await DBNacbar.Estado.Select(p => new Estado()
            {
                IdEstado = p.IdEstado,
                DsEstado = p.DsEstado
            }).AsNoTracking().ToListAsync();

            if (id != null)
            {
                Municipio = await DBNacbar.Municipio.AsNoTracking().FirstOrDefaultAsync(p => p.IdMunicipio == id);

                if (Municipio == null)
                    return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Municipio.IdMunicipio == 0)
            {
                Municipio.IdMunicipio = await DBNacbar.Municipio.MaxAsync(p => (System.Int32?)p.IdMunicipio + 1) ?? 1;
                Municipio.IdPais = DBNacbar.Estado.Where(p => p.IdEstado == Municipio.IdEstado).Select(p => p.IdPais).First();

                DBNacbar.Add(Municipio);
            }
            else
            {
                Municipio.IdPais = DBNacbar.Estado.Where(p => p.IdEstado == Municipio.IdEstado).Select(p => p.IdPais).First();
                DBNacbar.Municipio.Update(Municipio);
            }
            await DBNacbar.SaveChangesAsync();

            return RedirectToPage("UFLeitura");
        }

        [BindProperty]
        public Municipio Municipio { get; set; }
     
        [BindProperty]
        public System.Collections.Generic.IList<Estado> LstEstado{ get; set; }

    }
}