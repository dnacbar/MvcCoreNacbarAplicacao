using System;
using System.Collections.Generic;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Municipio
    {
        public Municipio()
        {
            Empresa = new HashSet<Empresa>();
            Pessoa = new HashSet<Pessoa>();
            Pessoaendereco = new HashSet<Pessoaendereco>();
        }

        public int IdMunicipio { get; set; }
        public string IdPais { get; set; }
        public int IdEstado { get; set; }
        public string DsMunicipio { get; set; }
        public string CdMunicipio { get; set; }

        public Estado Id { get; set; }
        public ICollection<Empresa> Empresa { get; set; }
        public ICollection<Pessoa> Pessoa { get; set; }
        public ICollection<Pessoaendereco> Pessoaendereco { get; set; }
    }
}
