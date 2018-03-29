using System;
using System.Collections.Generic;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Estadocivil
    {
        public Estadocivil()
        {
            Pessoa = new HashSet<Pessoa>();
        }

        public short IdEstadocivil { get; set; }
        public string DsEstadocivil { get; set; }

        public ICollection<Pessoa> Pessoa { get; set; }
    }
}
