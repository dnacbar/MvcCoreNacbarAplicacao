using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MvcCoreNacbarAplicacao.DBNacbarEF;
using System.Threading.Tasks;

namespace MvcCoreNacbarAplicacao.Pages
{
    public class BasicoCriarEditarModel : PageModel
    {
        DBNACBARContext DBNacbar;

        public BasicoCriarEditarModel(DBNACBARContext context)
        {
            DBNacbar = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id != null)
            {
                if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.BAIRRO)
                {
                    Bairro = await DBNacbar.Bairro.AsNoTracking().FirstOrDefaultAsync(p => p.IdBairro == id);

                    if (Bairro == null)
                        return NotFound();
                }
                else if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.COR)
                {
                    Cor = await DBNacbar.Cor.AsNoTracking().FirstOrDefaultAsync(p => p.IdCor == id);

                    if (Cor == null)
                        return NotFound();
                }
                else if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.ESTADOCIVIL)
                {
                    Estadocivil = await DBNacbar.Estadocivil.AsNoTracking().FirstOrDefaultAsync(p => p.IdEstadocivil == id);

                    if (Estadocivil == null)
                        return NotFound();
                }
                else if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.FORMAPAGAMENTO)
                {
                    Formapagamento = await DBNacbar.Formapagamento.AsNoTracking().FirstOrDefaultAsync(p => p.IdFormapagamento == id);

                    if (Formapagamento == null)
                        return NotFound();
                }
                else if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.MARCA)
                {
                    Marca = await DBNacbar.Marca.AsNoTracking().FirstOrDefaultAsync(p => p.IdMarca == id);

                    if (Marca == null)
                        return NotFound();
                }
                else if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.MOTIVOBLOQUEIO)
                {
                    Motivobloqueio = await DBNacbar.Motivobloqueio.AsNoTracking().FirstOrDefaultAsync(p => p.IdBloqueio == id);

                    if (Motivobloqueio == null)
                        return NotFound();
                }
                else if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.SERVICO)
                {
                    Servico = await DBNacbar.Servico.AsNoTracking().FirstOrDefaultAsync(p => p.IdServico == id);

                    if (Servico == null)
                        return NotFound();
                }
                else if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.TAMANHO)
                {
                    Tamanho = await DBNacbar.Tamanho.AsNoTracking().FirstOrDefaultAsync(p => p.IdTamanho == id);

                    if (Tamanho == null)
                        return NotFound();
                }
                else if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.TIPOPESSOA)
                {
                    Tipopessoa = await DBNacbar.Tipopessoa.AsNoTracking().FirstOrDefaultAsync(p => p.IdTipopessoa == id);

                    if (Tipopessoa == null)
                        return NotFound();
                }
                else if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.TIPOPRODUTO)
                {
                    Tipoproduto = await DBNacbar.Tipoproduto.AsNoTracking().FirstOrDefaultAsync(p => p.IdTipoproduto == id);

                    if (Tipoproduto == null)
                        return NotFound();
                }
                else if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.UNIDADE)
                {
                    Unidade = await DBNacbar.Unidade.AsNoTracking().FirstOrDefaultAsync(p => p.IdUnidade == id);

                    if (Unidade == null)
                        return NotFound();
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.BAIRRO)
            {
                if (Bairro.IdBairro == 0)
                {
                    Bairro.IdBairro = await DBNacbar.Bairro.MaxAsync(p => (System.Int32?)p.IdBairro + 1) ?? 1;
                    DBNacbar.Add(Bairro);
                }
                else
                    DBNacbar.Bairro.Update(Bairro);
            }
            else if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.COR)
            {
                if (Cor.IdCor == 0)
                {
                    Cor.IdCor = await DBNacbar.Cor.MaxAsync(p => (System.Int32?)p.IdCor + 1) ?? 1;
                    DBNacbar.Add(Cor);
                }
                else
                    DBNacbar.Cor.Update(Cor);
            }
            else if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.ESTADOCIVIL)
            {
                if (Estadocivil.IdEstadocivil == 0)
                {
                    Estadocivil.IdEstadocivil = (short?)await DBNacbar.Estadocivil.MaxAsync(p => (short?)p.IdEstadocivil + 1) ?? 1;
                    DBNacbar.Add(Estadocivil);
                }
                else
                    DBNacbar.Estadocivil.Update(Estadocivil);
            }
            else if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.FORMAPAGAMENTO)
            {
                if (Formapagamento.IdFormapagamento == 0)
                {
                    Formapagamento.IdFormapagamento = await DBNacbar.Formapagamento.MaxAsync(p => (System.Int32?)p.IdFormapagamento + 1) ?? 1;
                    DBNacbar.Add(Formapagamento);
                }
                else
                    DBNacbar.Formapagamento.Update(Formapagamento);
            }
            else if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.MARCA)
            {
                if (Marca.IdMarca == 0)
                {
                    Marca.IdMarca = await DBNacbar.Marca.MaxAsync(p => (System.Int32?)p.IdMarca + 1) ?? 1;
                    DBNacbar.Add(Marca);
                }
                else
                    DBNacbar.Marca.Update(Marca);
            }
            else if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.MOTIVOBLOQUEIO)
            {
                if (Motivobloqueio.IdBloqueio == 0)
                {
                    Motivobloqueio.IdBloqueio = await DBNacbar.Motivobloqueio.MaxAsync(p => (System.Int32?)p.IdBloqueio + 1) ?? 1;
                    DBNacbar.Add(Motivobloqueio);
                }
                else
                    DBNacbar.Motivobloqueio.Update(Motivobloqueio);
            }
            else if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.SERVICO)
            {
                if (Servico.IdServico == 0)
                {
                    Servico.IdServico = await DBNacbar.Servico.MaxAsync(p => (System.Int32?)p.IdServico + 1) ?? 1;
                    DBNacbar.Add(Servico);
                }
                else
                    DBNacbar.Servico.Update(Servico);
            }
            else if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.TAMANHO)
            {
                if (Tamanho.IdTamanho == 0)
                {
                    Tamanho.IdTamanho = await DBNacbar.Tamanho.MaxAsync(p => (System.Int32?)p.IdTamanho + 1) ?? 1;
                    DBNacbar.Add(Tamanho);
                }
                else
                    DBNacbar.Tamanho.Update(Tamanho);
            }
            else if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.TIPOPESSOA)
            {

                if (Tipopessoa.IdTipopessoa == 0)
                {
                    Tipopessoa.IdTipopessoa = await DBNacbar.Tipopessoa.MaxAsync(p => (System.Int32?)p.IdTipopessoa + 1) ?? 1;
                    DBNacbar.Add(Tipopessoa);
                }
                else
                    DBNacbar.Tipopessoa.Update(Tipopessoa);
            }
            else if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.TIPOPRODUTO)
            {
                if (Tipoproduto.IdTipoproduto == 0)
                {
                    Tipoproduto.IdTipoproduto = await DBNacbar.Tipoproduto.MaxAsync(p => (System.Int32?)p.IdTipoproduto + 1) ?? 1;
                    DBNacbar.Add(Tipoproduto);
                }
                else
                    DBNacbar.Tipoproduto.Update(Tipoproduto);
            }
            else if (BasicoLeituraModel.EnumBasico == BasicoLeituraModel.enumBasico.UNIDADE)
            {
                if (Unidade.IdUnidade == 0)
                {
                    Unidade.IdUnidade = await DBNacbar.Unidade.MaxAsync(p => (System.Int32?)p.IdUnidade + 1) ?? 1;
                    DBNacbar.Add(Unidade);
                }
                else
                    DBNacbar.Unidade.Update(Unidade);
            }

            await DBNacbar.SaveChangesAsync();

            return RedirectToPage("BasicoLeitura", new { EnumBasico = BasicoLeituraModel.EnumBasico });
        }

        [BindProperty]
        public Bairro Bairro { get; set; }

        [BindProperty]
        public Cor Cor { get; set; }

        [BindProperty]
        public Estadocivil Estadocivil { get; set; }

        [BindProperty]
        public Formapagamento Formapagamento { get; set; }

        [BindProperty]
        public Marca Marca { get; set; }

        [BindProperty]
        public Motivobloqueio Motivobloqueio { get; set; }

        [BindProperty]
        public Servico Servico { get; set; }

        [BindProperty]
        public Tamanho Tamanho { get; set; }

        [BindProperty]
        public Tipopessoa Tipopessoa { get; set; }

        [BindProperty]
        public Tipoproduto Tipoproduto { get; set; }

        [BindProperty]
        public Unidade Unidade { get; set; }

    }
}