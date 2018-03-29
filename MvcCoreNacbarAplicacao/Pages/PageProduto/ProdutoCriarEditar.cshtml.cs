using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MvcCoreNacbarAplicacao.DBNacbarEF;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNacbarAplicacao.Pages
{
    public class ProdutoCriarEditarModel : PageModel
    {
        public DBNACBARContext DBNacbar;

        public ProdutoCriarEditarModel(DBNACBARContext context)
        {
            DBNacbar = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            LstCor = await DBNacbar.Cor.OrderBy(p => p.DsCor).AsNoTracking().ToListAsync();

            LstMarca = await DBNacbar.Marca.OrderBy(p => p.DsMarca).AsNoTracking().ToListAsync();

            LstTamanho = await DBNacbar.Tamanho.OrderBy(p => p.DsTamanho).AsNoTracking().ToListAsync();

            LstTipoProduto = await DBNacbar.Tipoproduto.OrderBy(p => p.DsTipoproduto).AsNoTracking().ToListAsync();

            LstUnidade = await DBNacbar.Unidade.OrderBy(p => p.DsUnidade).AsNoTracking().ToListAsync();

            if (id != null)
            {
                ViewData["Edit"] = await DBNacbar.Produto.Where(p => p.IdEmpresa == NacbarAplicacao.IntIdEmpresa && p.IdProduto == id).CountAsync();

                if (System.Convert.ToInt32(ViewData["Edit"]) == 0)
                    return NotFound();

                ViewData["Edit"] = id;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Produto.IdProduto == 0)
            {
                Produto.IdEmpresa = NacbarAplicacao.IntIdEmpresa;
                Produto.IdProduto = await DBNacbar.Produto.Where(p => p.IdEmpresa == NacbarAplicacao.IntIdEmpresa).MaxAsync(p => (System.Int32?)p.IdProduto + 1) ?? 1;

                Produto.DtCadastro = Produto.DtAtualizacao = System.DateTime.Now;

                DBNacbar.Produto.Add(Produto);
            }
            else
            {
                Produto.DtAtualizacao = System.DateTime.Today;

                DBNacbar.Produto.Update(Produto);
            }

            await DBNacbar.SaveChangesAsync();

            return RedirectToPage("ProdutoLeitura");
        }

        [BindProperty]
        public Produto Produto { get; set; }

        [BindProperty]
        public IList<Cor> LstCor { get; set; }

        [BindProperty]
        public IList<Marca> LstMarca { get; set; }

        [BindProperty]
        public IList<Tamanho> LstTamanho { get; set; }

        [BindProperty]
        public IList<Tipoproduto> LstTipoProduto { get; set; }

        [BindProperty]
        public IList<Unidade> LstUnidade { get; set; }
    }
}