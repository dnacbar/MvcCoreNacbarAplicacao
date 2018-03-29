using System;
using System.Collections.Generic;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Agendaproduto
    {
        public int IdEmpresa { get; set; }
        public int IdAgenda { get; set; }
        public int IdAgendaproduto { get; set; }
        public int IdProduto { get; set; }
        public decimal NmQuantidade { get; set; }

        public Agenda Id { get; set; }
        public Produto IdNavigation { get; set; }
    }
}
