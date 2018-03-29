using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MvcCoreNacbarAplicacao.DBNacbarEF;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNacbarAplicacao.Pages
{
    public class PessoaCriarEditarModel : PageModel
    {
        public DBNACBARContext DBNacbar;

        public PessoaCriarEditarModel(DBNACBARContext context)
        {
            DBNacbar = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            LstEstadoCivil = await DBNacbar.Estadocivil.Select(p => new Estadocivil()
            {
                IdEstadocivil = p.IdEstadocivil,
                DsEstadocivil = p.DsEstadocivil
            }).OrderBy(p => p.DsEstadocivil).ToListAsync();

            LstEstado = await DBNacbar.Estado.Select(p => new Estado()
            {
                IdEstado = p.IdEstado,
                DsEstado = p.DsEstado + "-" + p.DsUf
            }).OrderBy(p => p.DsEstado).AsNoTracking().ToListAsync();

            LstBairro = await DBNacbar.Bairro.Select(p => new Bairro()
            {
                IdBairro = p.IdBairro,
                DsBairro = p.DsBairro
            }).AsNoTracking().ToListAsync();

            if (id != null)
            {
                Pessoa = await DBNacbar.Pessoa.FirstOrDefaultAsync(p => p.IdEmpresa == NacbarAplicacao.IntIdEmpresa && p.IdPessoa == id);

                if (Pessoa == null)
                    return NotFound();

                Pessoa.DsCep.FormataTexto(NacbarAplicacao.EnumFormato.CEP);
                Pessoa.DsTelefone.FormataTexto(NacbarAplicacao.EnumFormato.TELEFONE);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Pessoa.IdPessoa == 0)
            {
                Pessoa.IdEmpresa = NacbarAplicacao.IntIdEmpresa;
                Pessoa.IdPessoa = await DBNacbar.Pessoa.MaxAsync(p => (System.Int32?)p.IdPessoa + 1) ?? 1;
                Pessoa.DsCep = Pessoa.DsCep.RemoveFormato();
                Pessoa.DsTelefone = Pessoa.DsTelefone.RemoveFormato();
                Pessoa.DtCadastro = Pessoa.DtAtualizacao = System.DateTime.Now;
                Pessoa.BoTipopessoa = System.Convert.ToBoolean(PessoaLeituraModel.EnumPessoa);

                DBNacbar.Pessoa.Add(Pessoa);
            }
            else
            {
                Pessoa.DsCep = Pessoa.DsCep.RemoveFormato();
                Pessoa.DsTelefone = Pessoa.DsTelefone.RemoveFormato();
                Pessoa.DtAtualizacao = System.DateTime.Now;

                DBNacbar.Pessoa.Update(Pessoa);
            }

            await DBNacbar.SaveChangesAsync();

            return RedirectToPage("PessoaLeitura", new { PessoaLeituraModel.EnumPessoa });
        }

        [BindProperty]
        public Pessoa Pessoa { get; set; }

        [BindProperty]
        public IList<Estadocivil> LstEstadoCivil { get; set; }

        [BindProperty]
        public IList<Estado> LstEstado { get; set; }

        [BindProperty]
        public IList<Bairro> LstBairro { get; set; }
    }
}