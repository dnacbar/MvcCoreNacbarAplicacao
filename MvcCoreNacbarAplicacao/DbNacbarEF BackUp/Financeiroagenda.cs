using System;
using System.Collections.Generic;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Financeiroagenda
    {
        public int IdEmpresa { get; set; }
        public int IdFinanceiro { get; set; }
        public int IdAgenda { get; set; }

        public Agenda Id { get; set; }
        public Financeiro IdNavigation { get; set; }
    }
}
