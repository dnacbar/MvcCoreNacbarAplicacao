using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MvcCoreNacbarAplicacao.DBNacbarEF;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNacbarAplicacao.Pages
{
    public class AgendaFinanceiroModel : PageModel
    {

        DBNACBARContext DBNacbar;

        public AgendaFinanceiroModel(DBNACBARContext context)
        {
            DBNacbar = context;
        }

        public async Task<IActionResult> OnGetAsync()//int intAgenda)
        {
            LstFormapagamento = await DBNacbar.Formapagamento.Select(p => new Formapagamento
            {
                IdFormapagamento = p.IdFormapagamento,
                DsFormapagamento = p.DsFormapagamento
            }).AsNoTracking().ToListAsync();

            Financeiro = new Financeiro();
            //Agenda = await DBNacbar.Agenda.Where(p => p.IdEmpresa == NacbarAplicacao.IdEmpresa && p.IdAgenda == intAgenda).FirstAsync();
            //
            //Financeiroagenda Financeiroagenda = await DBNacbar.Financeiroagenda.Where(p => p.IdEmpresa == p.IdEmpresa && p.IdAgenda == Agenda.IdAgenda).FirstAsync();
            //
            //Financeiro = await DBNacbar.Financeiro.FirstAsync();

            return Page();
        }

        public IActionResult OnPostAsync()
        {
            Financeiro.DtPagamento = System.DateTime.Now;
            Financeiro.CdSituacao = (short)FinanceiroCriarEditarModel.enumStatusSituacao.Concluido;
            Financeiro.DsHistorico = "";

            DBNacbar.Update(Financeiro);
            return RedirectToPage("AgendaLeitura");
        }

        [BindProperty]
        public Financeiro Financeiro{ get; set; }

        [BindProperty]
        public Agenda Agenda { get; set; }

        [BindProperty]
        public IList<Formapagamento> LstFormapagamento { get; set; }
    }
}