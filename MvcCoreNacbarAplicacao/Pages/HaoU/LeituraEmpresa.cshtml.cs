using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MvcCoreNacbarAplicacao.DBNacbarEF;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNacbarAplicacao.Pages
{
    public class EmpresaLeituraModel : PageModel
    {
        private readonly DBNACBARContext DBNacbar;

        public EmpresaLeituraModel(DBNACBARContext context)
        {
            DBNacbar = context;
        }

        public async Task OnGetAsync()
        {
            LstEmpresa = await DBNacbar.Empresa.Select(p => new Empresa()
            {
                IdEmpresa = p.IdEmpresa,
                DsNomeempresa = p.DsNomeempresa,
                DsNomefantasia = p.DsNomefantasia,
                DsInscricaofederal = p.DsInscricaofederal,
                IdBairroNavigation = new Bairro { DsBairro = p.IdBairroNavigation.DsBairro },
                IdMunicipioNavigation = new Municipio { DsMunicipio = p.IdMunicipioNavigation.DsMunicipio },
                DsTelefone = p.DsTelefone,
                DsEmail = p.DsEmail,
                DtAtualizacao = p.DtAtualizacao,
            }).AsNoTracking().ToListAsync();
        }

        [BindProperty]
        public IList<Empresa> LstEmpresa { get; set; }
    }
}