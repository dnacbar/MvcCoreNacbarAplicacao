using System;
using System.Collections.Generic;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Financeiro
    {
        public Financeiro()
        {
            Financeiroagenda = new HashSet<Financeiroagenda>();
            Financeirohorario = new HashSet<Financeirohorario>();
        }

        public int IdEmpresa { get; set; }
        public int IdFinanceiro { get; set; }
        public bool BoOperacao { get; set; }
        public short CdSituacao { get; set; }
        public string DsHistorico { get; set; }
        public DateTime DtCriacao { get; set; }
        public DateTime? DtPagamento { get; set; }
        public DateTime DtVencimento { get; set; }
        public int? IdFormapagamento { get; set; }
        public decimal? NmDesconto { get; set; }
        public decimal? NmJuros { get; set; }
        public decimal NmSaldo { get; set; }
        public decimal NmValor { get; set; }
        public decimal? NmValorpago { get; set; }

        public Formapagamento IdFormapagamentoNavigation { get; set; }
        public ICollection<Financeiroagenda> Financeiroagenda { get; set; }
        public ICollection<Financeirohorario> Financeirohorario { get; set; }
    }
}
