using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MvcCoreNacbarAplicacao.DBNacbarEF;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace MvcCoreNacbarAplicacao.Pages
{
    public class RelatorioModel : PageModel
    {
        DBNACBARContext DBNacbar;

        public enum enumUF
        {
            PAIS,
            ESTADO,
            MUNICIPIO
        }

        public static enumUF EnumUf { get; set; }

        public RelatorioModel(DBNACBARContext context)
        {
            DBNacbar = context;
        }
    }
}