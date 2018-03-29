using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MvcCoreNacbarAplicacao.DBNacbarEF;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace MvcCoreNacbarAplicacao.Pages
{
    public class BasicoLeituraModel : PageModel
    {
        DBNACBARContext DBNacbar;

        public enum enumBasico
        {
            BAIRRO,
            COR,
            ESTADOCIVIL,
            FORMAPAGAMENTO,
            MARCA,
            MOTIVOBLOQUEIO,
            SERVICO,
            TAMANHO,
            TIPOPESSOA,
            TIPOPRODUTO,
            UNIDADE
        }

        public static enumBasico EnumBasico { get; set; }

        public BasicoLeituraModel(DBNACBARContext context)
        {
            DBNacbar = context;
        }

        public async Task<IActionResult> OnGetAsync(enumBasico Enumbasico)
        {
            if (Enumbasico == enumBasico.BAIRRO)
            {
                EnumBasico = enumBasico.BAIRRO;
                LstBairro = await DBNacbar.Bairro.Select(b => new Bairro { IdBairro = b.IdBairro, DsBairro = b.DsBairro }).AsNoTracking().ToListAsync();
            }
            else if (Enumbasico == enumBasico.COR)
            {
                EnumBasico = enumBasico.COR;
                LstCor = await DBNacbar.Cor.Select(c => new Cor { IdCor = c.IdCor, DsCor = c.DsCor }).AsNoTracking().ToListAsync();
            }
            else if (Enumbasico == enumBasico.ESTADOCIVIL)
            {
                EnumBasico = enumBasico.ESTADOCIVIL;
                LstEstadocivil = await DBNacbar.Estadocivil.Select(e => new Estadocivil { IdEstadocivil = e.IdEstadocivil, DsEstadocivil = e.DsEstadocivil }).AsNoTracking().ToListAsync();
            }
            else if (Enumbasico == enumBasico.FORMAPAGAMENTO)
            {
                EnumBasico = enumBasico.FORMAPAGAMENTO;
                LstFormapagamento = await DBNacbar.Formapagamento.Select(f => new Formapagamento { IdFormapagamento = f.IdFormapagamento, DsFormapagamento = f.DsFormapagamento }).AsNoTracking().ToListAsync();
            }
            else if (Enumbasico == enumBasico.MARCA)
            {
                EnumBasico = enumBasico.MARCA;
                LstMarca = await DBNacbar.Marca.Select(m => new Marca { IdMarca = m.IdMarca, DsMarca = m.DsMarca }).AsNoTracking().ToListAsync();
            }
            else if (Enumbasico == enumBasico.MOTIVOBLOQUEIO)
            {
                EnumBasico = enumBasico.MOTIVOBLOQUEIO;
                LstMotivobloqueio = await DBNacbar.Motivobloqueio.Select(m => new Motivobloqueio { IdBloqueio = m.IdBloqueio, DsMotivobloqueio = m.DsMotivobloqueio }).AsNoTracking().ToListAsync();
            }
            else if (Enumbasico == enumBasico.SERVICO)
            {
                EnumBasico = enumBasico.SERVICO;
                LstServico = await DBNacbar.Servico.Select(s => new Servico { IdServico = s.IdServico, DsServico = s.DsServico, NmValor = s.NmValor }).AsNoTracking().ToListAsync();
            }
            else if (Enumbasico == enumBasico.TAMANHO)
            {
                EnumBasico = enumBasico.TAMANHO;
                LstTamanho = await DBNacbar.Tamanho.Select(t => new Tamanho { IdTamanho = t.IdTamanho, DsTamanho = t.DsTamanho }).AsNoTracking().ToListAsync();
            }
            else if (Enumbasico == enumBasico.TIPOPESSOA)
            {
                EnumBasico = enumBasico.TIPOPESSOA;
                LstTipopessoa = await DBNacbar.Tipopessoa.Select(t => new Tipopessoa { IdTipopessoa = t.IdTipopessoa, DsTipopessoa = t.DsTipopessoa }).AsNoTracking().ToListAsync();
            }
            else if (Enumbasico == enumBasico.TIPOPRODUTO)
            {
                EnumBasico = enumBasico.TIPOPRODUTO;
                LstTipoproduto = await DBNacbar.Tipoproduto.Select(t => new Tipoproduto { IdTipoproduto = t.IdTipoproduto, DsTipoproduto = t.DsTipoproduto }).AsNoTracking().ToListAsync();
            }
            else if (Enumbasico == enumBasico.UNIDADE)
            {
                EnumBasico = enumBasico.UNIDADE;
                LstUnidade = await DBNacbar.Unidade.Select(u => new Unidade { IdUnidade = u.IdUnidade, DsUnidade = u.DsUnidade }).AsNoTracking().ToListAsync();
            }
            return Page();
        }

        [BindProperty]
        public IList<Bairro> LstBairro { get; set; }

        [BindProperty]
        public IList<Cor> LstCor { get; set; }

        [BindProperty]
        public IList<Estadocivil> LstEstadocivil { get; set; }

        [BindProperty]
        public IList<Formapagamento> LstFormapagamento { get; set; }

        [BindProperty]
        public IList<Marca> LstMarca { get; set; }

        [BindProperty]
        public IList<Motivobloqueio> LstMotivobloqueio { get; set; }
        
        [BindProperty]
        public IList<Servico> LstServico { get; set; }

        [BindProperty]
        public IList<Tamanho> LstTamanho { get; set; }
        
        [BindProperty]    
        public IList<Tipopessoa> LstTipopessoa { get; set; }
            
        [BindProperty]
        public IList<Tipoproduto> LstTipoproduto { get; set; }
        
        [BindProperty]
        public IList<Unidade> LstUnidade { get; set; }
    }
}