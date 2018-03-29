using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MvcCoreNacbarAplicacao.DBNacbarEF;
using System.Threading.Tasks;

namespace MvcCoreNacbarAplicacao.Pages
{
    public class BasicoExcluirModel : PageModel
    {
        public DBNACBARContext DBNacbar;

        public BasicoExcluirModel(DBNACBARContext context)
        {
            DBNacbar = context;
        }

        public IActionResult OnGet(int IntId, string StrDescricao)
        {

            this.IntId = IntId;
            this.StrDescricao = StrDescricao;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
                return Page();

            if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.BAIRRO)
                DBNacbar.Bairro.Remove(await DBNacbar.Bairro.FirstOrDefaultAsync(p => p.IdBairro == id));
            else if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.COR)
                DBNacbar.Cor.Remove(await DBNacbar.Cor.FirstOrDefaultAsync(p => p.IdCor == id));
            else if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.ESTADOCIVIL)
                DBNacbar.Estadocivil.Remove(await DBNacbar.Estadocivil.FirstOrDefaultAsync(p => p.IdEstadocivil == id));
            else if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.FORMAPAGAMENTO)
                DBNacbar.Formapagamento.Remove(await DBNacbar.Formapagamento.FirstOrDefaultAsync(p => p.IdFormapagamento == id));
            else if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.MARCA)
                DBNacbar.Marca.Remove(await DBNacbar.Marca.FirstOrDefaultAsync(p => p.IdMarca == id));
            else if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.MOTIVOBLOQUEIO)
                DBNacbar.Motivobloqueio.Remove(await DBNacbar.Motivobloqueio.FirstOrDefaultAsync(p => p.IdBloqueio == id));
            else if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.SERVICO)
                DBNacbar.Servico.Remove(await DBNacbar.Servico.FirstOrDefaultAsync(p => p.IdServico == id));
            else if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.TAMANHO)
                DBNacbar.Tamanho.Remove(await DBNacbar.Tamanho.FirstOrDefaultAsync(p => p.IdTamanho == id));
            else if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.TIPOPESSOA)
                DBNacbar.Tipopessoa.Remove(await DBNacbar.Tipopessoa.FirstOrDefaultAsync(p => p.IdTipopessoa == id));
            else if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.TIPOPRODUTO)
                DBNacbar.Tipoproduto.Remove(await DBNacbar.Tipoproduto.FirstOrDefaultAsync(p => p.IdTipoproduto == id));
            else if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.UNIDADE)
                DBNacbar.Unidade.Remove(await DBNacbar.Unidade.FirstOrDefaultAsync(p => p.IdUnidade == id));

            await DBNacbar.SaveChangesAsync();

            return RedirectToPage("BasicoLeitura", new { EnumBasico = BasicoLeituraModel.EnumBasico });
        }

        [BindProperty]
        public int IntId { get; set; }

        [BindProperty]
        public string StrDescricao { get; set; }
    }
}