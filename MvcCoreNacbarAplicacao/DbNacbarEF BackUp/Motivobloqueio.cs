using System;
using System.Collections.Generic;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Motivobloqueio
    {
        public Motivobloqueio()
        {
            Pessoa = new HashSet<Pessoa>();
            Produto = new HashSet<Produto>();
        }

        public int IdBloqueio { get; set; }
        public string DsMotivobloqueio { get; set; }

        public ICollection<Pessoa> Pessoa { get; set; }
        public ICollection<Produto> Produto { get; set; }
    }
}
