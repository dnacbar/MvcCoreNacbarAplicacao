using System;
using System.Collections.Generic;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Financeirohorario
    {
        public int IdEmpresa { get; set; }
        public int IdFinanceiro { get; set; }
        public int IdCliente { get; set; }
        public int IdProfissional { get; set; }
        public int IdProfissao { get; set; }
        public int IdServico { get; set; }

        public Financeiro Id { get; set; }
    }
}
