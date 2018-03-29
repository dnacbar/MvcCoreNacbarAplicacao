using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MvcCoreNacbarAplicacao.DBNacbarEF;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNacbarAplicacao.Pages
{
    public class PessoaBloquearModel : PageModel
    {
        public DBNACBARContext DBNacbar;
        public PessoaBloquearModel(DBNACBARContext context)
        {
            DBNacbar = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            LstMotivoBloqueio = await DBNacbar.Motivobloqueio.OrderBy(p => p.DsMotivobloqueio).ToListAsync();

            Pessoa = await DBNacbar.Pessoa.Where(p => p.IdEmpresa == NacbarAplicacao.IntIdEmpresa 
                                                   && p.IdPessoa == id).FirstOrDefaultAsync();

            if (Pessoa == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            DBNacbar.Pessoa.Update(Pessoa);
            await DBNacbar.SaveChangesAsync();

            return RedirectToPage("PessoaLeitura", new { EnumPessoa = PessoaLeituraModel.EnumPessoa });
        }

        [BindProperty]
        public Pessoa Pessoa { get; set; }

        [BindProperty]
        public System.Collections.Generic.IList<Motivobloqueio> LstMotivoBloqueio { get; set; }
    }
}