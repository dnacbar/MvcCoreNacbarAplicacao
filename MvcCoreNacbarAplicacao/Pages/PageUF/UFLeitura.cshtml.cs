using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MvcCoreNacbarAplicacao.DBNacbarEF;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace MvcCoreNacbarAplicacao.Pages
{
    public class UFLeituraModel : PageModel
    {
        DBNACBARContext DBNacbar;

        //public enum enumUF
        //{
        //    PAIS,
        //    ESTADO,
        //    MUNICIPIO
        //}

        //public static enumUF EnumUf { get; set; }

        public UFLeituraModel(DBNACBARContext context)
        {
            DBNacbar = context;
        }

        public async Task<IActionResult> OnGetAsync()//enumUF EnumUF)
        {
            LstEstado = await DBNacbar.Estado.Select(p => new Estado()
            {
                IdEstado = p.IdEstado,
                DsEstado = p.DsEstado + "-" + p.DsUf
            }).OrderBy(p => p.DsEstado).AsNoTracking().ToListAsync();

            #region 
            //if (EnumUF == enumUF.PAIS)
            //{
            //    EnumUf = enumUF.PAIS;
            //    LstPais = await DBNacbar.Pais.Select(p => new Pais() { IdPais = p.IdPais, DsPais = p.DsPais }).AsNoTracking().ToListAsync();
            //}
            //else if (EnumUF == enumUF.ESTADO)
            //{
            //    EnumUf = enumUF.ESTADO;

            //}
            //else if (EnumUF == enumUF.MUNICIPIO)
            //{
            //EnumUf = enumUF.MUNICIPIO;
            //LstMunicipio = await DBNacbar.Municipio.Select(p => new Municipio()
            //{
            //    IdMunicipio = p.IdMunicipio,
            //    CdMunicipio = p.CdMunicipio,
            //    DsMunicipio = p.DsMunicipio,
            //    Id = new Estado() { DsUf = p.Id.DsUf }
            //}).AsNoTracking().OrderBy(p => p.DsMunicipio).ThenBy(p => p.Id.DsUf).ToListAsync();
            //}
            #endregion

            return Page();
        }

        [BindProperty]
        public IList<Estado> LstEstado { get; set; }

        //[BindProperty]
        //public IList<Pais> LstPais { get; set; }

        //[BindProperty]
        //public IList<Municipio> LstMunicipio { get; set; }
    }
}