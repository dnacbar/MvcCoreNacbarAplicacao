using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MvcCoreNacbarAplicacao.DBNacbarEF;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace MvcCoreNacbarAplicacao.Pages
{
    public class FinanceiroCriarEditarModel : PageModel
    {
        DBNACBARContext DBNacbar;

        public enum enumStatusSituacao : short
        {
            Pendente = 0,
            Concluido = 1,
            Cancelado = 2,
            Renegociado = 3,
            Baixado = 4,
            Estornado = 5
        }

        public static enumStatusSituacao EnumStatusSituacao { get; set; }

        public FinanceiroCriarEditarModel(DBNACBARContext context)
        {
            DBNacbar = context;
        }
    }
}