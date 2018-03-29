using System;
using System.Collections.Generic;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Vendaitem
    {
        public int IdEmpresa { get; set; }
        public int IdVenda { get; set; }
        public int IdVendaitem { get; set; }
        public int IdProduto { get; set; }
        public decimal? NmAcrescimounitario { get; set; }
        public decimal? NmDescontounitario { get; set; }
        public decimal NmQuantidade { get; set; }
        public decimal? NmQuantidadedevolvida { get; set; }
        public decimal NmTotal { get; set; }
        public decimal NmValorfinalunitario { get; set; }
        public decimal NmValorunitario { get; set; }

        public Produto Id { get; set; }
        public Venda IdNavigation { get; set; }
    }
}
