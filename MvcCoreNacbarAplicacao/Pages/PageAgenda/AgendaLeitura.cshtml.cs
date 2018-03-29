using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MvcCoreNacbarAplicacao.DBNacbarEF;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNacbarAplicacao.Pages
{
    public class AgendaLeituraModel : PageModel
    {

        DBNACBARContext DBNacbar;

        public AgendaLeituraModel(DBNACBARContext context)
        {
            DBNacbar = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            //LstProfissional = await DBNacbar.Profissional
            //                                .Select(p => new Profissional
            //                                {
            //                                    IdEmpresa = p.IdEmpresa,
            //                                    IdProfissional = p.IdProfissional,
            //                                    IdProfissao = p.IdProfissao,
            //                                    IdServico = p.IdServico,
            //                                    Id = new Pessoa
            //                                    {
            //                                        DsNomepessoa = p.Id.DsNomepessoa + " - " +
            //                                                       p.IdProfissaoNavigation.DsTipopessoa + " - " +
            //                                                       p.IdServicoNavigation.DsServico
            //                                    }
            //                                })
            //                                .Where(p => DBNacbar.Horario
            //                                                    .Any(h => p.IdEmpresa == h.IdEmpresa
            //                                                           && p.IdProfissional == h.IdProfissional
            //                                                           && p.IdProfissao == h.IdProfissao
            //                                                           && p.IdServico == h.IdServico))
            //                                .AsNoTracking().ToListAsync();

            LstCliente = await DBNacbar.Pessoa
                                       .Select(p => new Pessoa
                                       {
                                           IdEmpresa = p.IdEmpresa,
                                           IdPessoa = p.IdPessoa,
                                           DsNomepessoa = p.DsNomepessoa
                                       }).Where(p => DBNacbar.Agenda
                                                             .Any(a => p.IdEmpresa == a.IdEmpresa
                                                                    && p.IdPessoa == a.IdCliente))
                                       .AsNoTracking().ToListAsync();

            return Page();
        }

        [BindProperty]
        public IList<Profissional> LstProfissional { get; set; }

        [BindProperty]
        public IList<Pessoa> LstCliente { get; set; }
    }
}