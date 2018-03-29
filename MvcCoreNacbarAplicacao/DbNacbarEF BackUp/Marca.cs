using System;
using System.Collections.Generic;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Marca
    {
        public Marca()
        {
            Produto = new HashSet<Produto>();
        }

        public int IdMarca { get; set; }
        public string DsMarca { get; set; }

        public ICollection<Produto> Produto { get; set; }
    }
}
