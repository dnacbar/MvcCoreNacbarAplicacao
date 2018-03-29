using System;
using System.Collections.Generic;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Pais
    {
        public Pais()
        {
            Estado = new HashSet<Estado>();
        }

        public string IdPais { get; set; }
        public string DsPais { get; set; }

        public ICollection<Estado> Estado { get; set; }
    }
}
