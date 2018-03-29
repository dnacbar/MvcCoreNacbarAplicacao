using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvcCoreNacbarAplicacao.DBNacbarEF;
using Microsoft.EntityFrameworkCore;

namespace MvcCoreNacbarAplicacao.Pages
{
    public class EmpresaCriarEditarModel : PageModel
    {
        private readonly DBNACBARContext DBNacbar;

        public EmpresaCriarEditarModel(DBNACBARContext context)
        {
            DBNacbar = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            LstMunicipio = await DBNacbar.Municipio.Select(p => new Municipio()
            {
                IdMunicipio = p.IdMunicipio,
                DsMunicipio = p.DsMunicipio + " - " + p.Id.DsUf
            }).AsNoTracking().ToListAsync();

            LstBairro = await DBNacbar.Bairro.Select(p => new Bairro()
            {
                IdBairro = p.IdBairro,
                DsBairro = p.DsBairro
            }).AsNoTracking().ToListAsync();

            if (id != null)
            {
                Empresa = await DBNacbar.Empresa.AsNoTracking().FirstOrDefaultAsync(p => p.IdEmpresa == id);

                if (Empresa == null)
                    return NotFound();

                Empresa.DsCep.FormataTexto(NacbarAplicacao.EnumFormato.CEP);
                Empresa.DsTelefone.FormataTexto(NacbarAplicacao.EnumFormato.TELEFONE);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Empresa.IdEmpresa == 0)
            {
                Empresa.IdEmpresa = await DBNacbar.Empresa.MaxAsync(p => (System.Int32?)p.IdEmpresa + 1) ?? 1;
                Empresa.DsCep = Empresa.DsCep.RemoveFormato();
                Empresa.DsTelefone = Empresa.DsTelefone.RemoveFormato();
                Empresa.DtCadastro = Empresa.DtAtualizacao = System.DateTime.Now;
                
                DBNacbar.Empresa.Add(Empresa);
            }
            else
            {
                Empresa.DsCep = Empresa.DsCep;
                Empresa.DsTelefone = Empresa.DsTelefone;
                Empresa.DtAtualizacao = System.DateTime.Now;
                DBNacbar.Empresa.Update(Empresa);
            }


            await DBNacbar.SaveChangesAsync();

            return RedirectToPage("EmpresaLeitura");
        }

        [BindProperty]
        public Empresa Empresa { get; set; }

        [BindProperty]
        public IList<Municipio> LstMunicipio { get; set; }

        [BindProperty]
        public IList<Bairro> LstBairro { get; set; }
    }
}