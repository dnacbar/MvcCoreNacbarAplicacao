using System;
using System.Collections.Generic;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Unidade
    {
        public Unidade()
        {
            Produto = new HashSet<Produto>();
        }

        public int IdUnidade { get; set; }
        public string DsUnidade { get; set; }

        public ICollection<Produto> Produto { get; set; }
    }
}
