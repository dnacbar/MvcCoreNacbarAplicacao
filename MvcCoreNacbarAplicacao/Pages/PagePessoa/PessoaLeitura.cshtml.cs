using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MvcCoreNacbarAplicacao.DBNacbarEF;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace MvcCoreNacbarAplicacao.Pages
{
    public class PessoaLeituraModel : PageModel
    {
        DBNACBARContext DBNacbar;

        public enum enumPessoa : short
        {
            FISICA = 0,
            JURIDICA = 1
        }

        public static enumPessoa EnumPessoa { get; set; }

        public PessoaLeituraModel(DBNACBARContext context)
        {
            DBNacbar = context;
        }

        public async Task<IActionResult> OnGetAsync(enumPessoa enumPessoa)
        {
            EnumPessoa = enumPessoa;

            if (enumPessoa == enumPessoa.FISICA)
            {
                LstPessoa = await DBNacbar.Pessoa.Select(p => new Pessoa
                {
                    IdEmpresa = p.IdEmpresa,
                    IdPessoa = p.IdPessoa,
                    DsNomepessoa = p.DsNomepessoa,
                    BoTipopessoa = p.BoTipopessoa
                }).Where(p => p.IdEmpresa == NacbarAplicacao.IntIdEmpresa && p.BoTipopessoa == false).AsNoTracking().ToListAsync();
            }
            else
            {
                LstPessoa = await DBNacbar.Pessoa.Select(p => new Pessoa
                {
                    IdEmpresa = p.IdEmpresa,
                    IdPessoa = p.IdPessoa,
                    DsNomepessoa = p.DsNomepessoa,
                    DsNomefantasia = p.DsNomefantasia,
                    BoTipopessoa = p.BoTipopessoa
                }).Where(p => p.IdEmpresa == NacbarAplicacao.IntIdEmpresa && p.BoTipopessoa == true).AsNoTracking().ToListAsync();
            }

            return Page();
        }

        [BindProperty]
        public IList<Pessoa> LstPessoa { get; set; }
    }
}