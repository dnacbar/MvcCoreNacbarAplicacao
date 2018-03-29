using System;
using System.Collections.Generic;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Bairro
    {
        public Bairro()
        {
            Empresa = new HashSet<Empresa>();
            Pessoa = new HashSet<Pessoa>();
            Pessoaendereco = new HashSet<Pessoaendereco>();
        }

        public int IdBairro { get; set; }
        public string DsBairro { get; set; }

        public ICollection<Empresa> Empresa { get; set; }
        public ICollection<Pessoa> Pessoa { get; set; }
        public ICollection<Pessoaendereco> Pessoaendereco { get; set; }
    }
}
