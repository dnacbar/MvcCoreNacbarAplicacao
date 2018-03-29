using System;
using System.Collections.Generic;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Tamanho
    {
        public Tamanho()
        {
            Produto = new HashSet<Produto>();
        }

        public int IdTamanho { get; set; }
        public string DsTamanho { get; set; }

        public ICollection<Produto> Produto { get; set; }
    }
}
