using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MvcCoreNacbarAplicacao.DBNacbarEF;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNacbarAplicacao.Pages
{
    public class AgendaCriarModel : PageModel
    {

        DBNACBARContext DBNacbar;

        public AgendaCriarModel(DBNACBARContext context)
        {
            DBNacbar = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["IdAgenda"] = DBNacbar.Agenda.Max(p => (System.Int32?)p.IdAgenda + 1) ?? 1;

            LstProfissional = await DBNacbar.Profissional
                                            .Select(p => new Profissional
                                            {
                                                IdEmpresa = p.IdEmpresa,
                                                IdProfissional = p.IdProfissional,
                                                IdProfissao = p.IdProfissao,
                                                IdServico = p.IdServico,
                                                Id = new Pessoa
                                                {
                                                    DsNomepessoa = p.Id.DsNomepessoa + " - " +
                                                                   p.IdProfissaoNavigation.DsTipopessoa + " - " +
                                                                   p.IdServicoNavigation.DsServico
                                                }
                                            }).Where(p => p.IdEmpresa == NacbarAplicacao.IntIdEmpresa)
                                            .AsNoTracking().ToListAsync();

            LstCliente = await DBNacbar.Pessoa
                                       .Select(p => new Pessoa
                                       {
                                           IdEmpresa = p.IdEmpresa,
                                           IdPessoa = p.IdPessoa,
                                           DsNomepessoa = p.DsNomepessoa
                                       }).Where(p => p.IdEmpresa == NacbarAplicacao.IntIdEmpresa
                                                  && p.BoTipopessoa == System.Convert.ToBoolean(PessoaLeituraModel.enumPessoa.FISICA))
                                       .AsNoTracking().ToListAsync();

            return Page();
        }

        public IActionResult OnPostAsync()
        {
            return RedirectToPage("AgendaLeitura");
        }

        [BindProperty]
        public IList<Profissional> LstProfissional { get; set; }

        [BindProperty]
        public IList<Pessoa> LstCliente { get; set; }
    }
}