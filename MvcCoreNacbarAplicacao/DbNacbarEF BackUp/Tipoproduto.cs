using System;
using System.Collections.Generic;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Tipoproduto
    {
        public Tipoproduto()
        {
            Produto = new HashSet<Produto>();
        }

        public int IdTipoproduto { get; set; }
        public string DsTipoproduto { get; set; }

        public ICollection<Produto> Produto { get; set; }
    }
}
