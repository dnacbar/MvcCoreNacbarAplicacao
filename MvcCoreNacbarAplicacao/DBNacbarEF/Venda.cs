using System;
using System.Collections.Generic;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Venda
    {
        public Venda()
        {
            Vendaitem = new HashSet<Vendaitem>();
        }

        public int IdEmpresa { get; set; }
        public int IdVenda { get; set; }
        public DateTime? DtCancelamento { get; set; }
        public DateTime? DtDataconclusao { get; set; }
        public DateTime DtDatavenda { get; set; }
        public int IdCliente { get; set; }
        public decimal? NmAcrescimo { get; set; }
        public decimal? NmDesconto { get; set; }
        public decimal? NmFrete { get; set; }
        public decimal NmSubtotal { get; set; }
        public decimal NmTotal { get; set; }

        public Pessoa Id { get; set; }
        public Empresa IdEmpresaNavigation { get; set; }
        public ICollection<Vendaitem> Vendaitem { get; set; }
    }
}
