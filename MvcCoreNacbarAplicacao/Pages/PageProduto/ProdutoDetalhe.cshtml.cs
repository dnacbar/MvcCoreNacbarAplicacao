using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MvcCoreNacbarAplicacao.DBNacbarEF;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace MvcCoreNacbarAplicacao.Pages
{
    public class ProdutoDetalheModel : PageModel
    {
        DBNACBARContext DBNacbar;


        public ProdutoDetalheModel(DBNACBARContext context)
        {
            DBNacbar = context;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Pessoa = await DBNacbar.Pessoa.Select(p => new Pessoa
            {
                IdEmpresa = p.IdEmpresa,
                IdPessoa = p.IdPessoa,
                DsNomepessoa = p.DsNomepessoa,
                DsNomefantasia = p.DsNomefantasia,
                DsInscricaofederal = p.DsInscricaofederal,
                DsInscricaoestadual = p.DsInscricaoestadual,
                DsInscricaomunicipal = p.DsInscricaomunicipal,
                DtNascimento = p.DtNascimento,
                DsCep = p.DsCep,
                DsLogradouro = p.DsLogradouro,
                DsComplemento = p.DsComplemento,
                DsNumero = p.DsNumero,
                DsTelefone = p.DsTelefone,
                DsCelular = p.DsCelular,
                DsEmail = p.DsEmail,
                DsResponsavel = p.DsResponsavel,
                DsTelefoneresponsavel = p.DsTelefoneresponsavel,
                DsCelularresponsavel = p.DsCelularresponsavel,
                IdBairroNavigation = new Bairro { DsBairro = p.IdBairroNavigation.DsBairro },
                IdEstadocivilNavigation = new Estadocivil { DsEstadocivil = p.IdEstadocivilNavigation.DsEstadocivil },
                IdMunicipioNavigation = new Municipio { DsMunicipio = p.IdMunicipioNavigation.DsMunicipio },
                BoTipopessoa = p.BoTipopessoa,
                DsObservacao = p.DsObservacao
            }).Where(p => p.IdEmpresa == NacbarAplicacao.IntIdEmpresa && p.IdPessoa == id).FirstAsync();

            if (Pessoa.DtNascimento.HasValue)
            {
                DateTime dtZero = new DateTime(1, 1, 1);

                TimeSpan tsData = DateTime.Today - Pessoa.DtNascimento.Value;

                BolMenor = (dtZero + tsData).Year - 1 <= 18 ? true : false;
            }
            return Page();
        }

        [BindProperty]
        public Pessoa Pessoa { get; set; }

        [BindProperty]
        public bool BolMenor { get; set; }
    }
}