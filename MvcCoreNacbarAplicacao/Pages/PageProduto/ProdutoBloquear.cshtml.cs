using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MvcCoreNacbarAplicacao.DBNacbarEF;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNacbarAplicacao.Pages
{
    public class ProdutoBloquearModel : PageModel
    {
        public DBNACBARContext DBNacbar;
        public ProdutoBloquearModel(DBNACBARContext context)
        {
            DBNacbar = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            LstMotivoBloqueio = await DBNacbar.Motivobloqueio.OrderBy(p => p.DsMotivobloqueio).AsNoTracking().ToListAsync();

            Produto = await DBNacbar.Produto.Where(p => p.IdEmpresa == NacbarAplicacao.IntIdEmpresa
                                                     && p.IdProduto == id).FirstOrDefaultAsync();

            if (Produto == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            DBNacbar.Produto.Update(Produto);

            await DBNacbar.SaveChangesAsync();

            return RedirectToPage("ProdutoLeitura");
        }

        [BindProperty]
        public Produto Produto { get; set; }

        [BindProperty]
        public System.Collections.Generic.IList<Motivobloqueio> LstMotivoBloqueio { get; set; }
    }
}