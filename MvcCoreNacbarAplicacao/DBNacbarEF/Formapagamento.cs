using System;
using System.Collections.Generic;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Formapagamento
    {
        public Formapagamento()
        {
            Financeiro = new HashSet<Financeiro>();
        }

        public int IdFormapagamento { get; set; }
        public string DsFormapagamento { get; set; }

        public ICollection<Financeiro> Financeiro { get; set; }
    }
}
