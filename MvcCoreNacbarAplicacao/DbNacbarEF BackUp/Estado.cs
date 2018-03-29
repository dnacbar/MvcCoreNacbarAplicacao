using System;
using System.Collections.Generic;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Estado
    {
        public Estado()
        {
            Municipio = new HashSet<Municipio>();
        }

        public string IdPais { get; set; }
        public int IdEstado { get; set; }
        public string DsEstado { get; set; }
        public string DsUf { get; set; }

        public Pais IdPaisNavigation { get; set; }
        public ICollection<Municipio> Municipio { get; set; }
    }
}
