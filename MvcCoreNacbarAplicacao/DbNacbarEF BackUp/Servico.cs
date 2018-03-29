using System;
using System.Collections.Generic;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Servico
    {
        public Servico()
        {
            Profissional = new HashSet<Profissional>();
        }

        public int IdServico { get; set; }
        public string DsServico { get; set; }
        public decimal? NmValor { get; set; }

        public ICollection<Profissional> Profissional { get; set; }
    }
}
