using System;
using System.Collections.Generic;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Tipopessoa
    {
        public Tipopessoa()
        {
            Profissional = new HashSet<Profissional>();
        }

        public int IdTipopessoa { get; set; }
        public string DsTipopessoa { get; set; }

        public ICollection<Profissional> Profissional { get; set; }
    }
}
