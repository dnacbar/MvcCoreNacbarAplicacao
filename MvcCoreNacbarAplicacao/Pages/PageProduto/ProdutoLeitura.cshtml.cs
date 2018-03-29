using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MvcCoreNacbarAplicacao.DBNacbarEF;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace MvcCoreNacbarAplicacao.Pages
{
    public class ProdutoLeituraModel : PageModel
    {
        DBNACBARContext DBNacbar;

        public ProdutoLeituraModel(DBNACBARContext context)
        {
            DBNacbar = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            LstProduto = await DBNacbar.Produto.Select(p => new Produto
            {
                IdEmpresa = p.IdEmpresa,
                IdProduto = p.IdProduto,
                DsNomeproduto = p.DsNomeproduto,
                IdBloqueioNavigation = new Motivobloqueio() { DsMotivobloqueio = p.IdBloqueioNavigation.DsMotivobloqueio },
            }).Where(p => p.IdEmpresa == NacbarAplicacao.IntIdEmpresa).AsNoTracking().ToListAsync();


            return Page();
        }

        [BindProperty]
        public IList<Produto> LstProduto { get; set; }
    }
}