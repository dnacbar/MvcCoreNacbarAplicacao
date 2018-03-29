using System;
using System.Collections.Generic;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Cor
    {
        public Cor()
        {
            Produto = new HashSet<Produto>();
        }

        public int IdCor { get; set; }
        public string DsCor { get; set; }

        public ICollection<Produto> Produto { get; set; }
    }
}
