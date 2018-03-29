using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MvcCoreNacbarAplicacao.DBNacbarEF;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNacbarAplicacao.Pages
{
    public class IndexModel : PageModel
    {
        DBNACBARContext DBNacbar;

        public IndexModel(DBNACBARContext context)
        {
            DBNacbar = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            LstEmpresa = await DBNacbar.Empresa
                                       .Select(p => new Empresa
                                       {
                                           IdEmpresa = p.IdEmpresa,
                                           DsNomeempresa = p.DsNomeempresa,
                                           DsNomefantasia = p.DsNomefantasia,
                                           BoAtiva = p.BoAtiva,
                                           DsImagem = p.DsImagem
                                       }).OrderBy(p => p.DsNomeempresa).Where(p => p.BoAtiva == true).ToListAsync();

            return Page();
        }

        [BindProperty]
        public IList<Empresa> LstEmpresa { get; set; }
    }
}
