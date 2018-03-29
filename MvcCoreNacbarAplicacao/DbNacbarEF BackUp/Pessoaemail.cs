using System;
using System.Collections.Generic;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Pessoaemail
    {
        public int IdEmpresa { get; set; }
        public int IdPessoa { get; set; }
        public int IdEmail { get; set; }
        public string DsContato { get; set; }
        public string DsEmail { get; set; }
        public DateTime? DtAtualizacao { get; set; }

        public Pessoa Id { get; set; }
    }
}
