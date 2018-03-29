using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MvcCoreNacbarAplicacao.DBNacbarEF;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNacbarAplicacao.Pages
{
    public class HorarioLeituraModel : PageModel
    {

        DBNACBARContext DBNacbar;

        public HorarioLeituraModel(DBNACBARContext context)
        {
            DBNacbar = context;
            DicDiaSemana = new Dictionary<string, string>
            {
                { "DOMINGO", "DOMINGO" },
                { "SEGUNDA-FEIRA", "SEGUNDA-FEIRA" },
                { "TERÇA-FEIRA", "TERÇA-FEIRA" },
                { "QUARTA-FEIRA", "QUARTA-FEIRA" },
                { "QUINTA-FEIRA", "QUINTA-FEIRA" },
                { "SEXTA-FEIRA", "SEXTA-FEIRA" },
                { "SÁBADO", "SÁBADO" }
            };

        }

        public async Task<IActionResult> OnGetAsync()
        {
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
                                            })
                                            .Where(p => DBNacbar.Horario
                                                                .Any(h => p.IdEmpresa == h.IdEmpresa
                                                                       && p.IdProfissional == h.IdProfissional
                                                                       && p.IdProfissao == h.IdProfissao
                                                                       && p.IdServico == h.IdServico))
                                            .AsNoTracking().ToListAsync();

            LstCliente = await DBNacbar.Pessoa
                              .Join(DBNacbar.Horario, p => new { p.IdEmpresa, p.IdPessoa }, h => new { h.IdEmpresa, h.Id.IdPessoa },
                              (p, h) => new { pessoa = p, horario = h })
                              .Select(p => new Pessoa
                              {
                                  IdEmpresa = p.pessoa.IdEmpresa,
                                  IdPessoa = p.pessoa.IdPessoa,
                                  DsNomepessoa = p.pessoa.DsNomepessoa,
                              }).Where(p => p.BoTipopessoa == false)
                              .AsNoTracking().ToListAsync();

            return Page();
        }

        [BindProperty]
        public IList<Profissional> LstProfissional { get; set; }

        [BindProperty]
        public IList<Pessoa> LstCliente { get; set; }

        [BindProperty]
        public Dictionary<string, string> DicDiaSemana { get; set; }

    }
}