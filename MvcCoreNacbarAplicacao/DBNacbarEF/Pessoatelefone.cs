using System;
using System.Collections.Generic;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Pessoatelefone
    {
        public int IdEmpresa { get; set; }
        public int IdPessoa { get; set; }
        public int IdTelefone { get; set; }
        public string DsContato { get; set; }
        public string DsTelefone { get; set; }
        public DateTime? DtAtualizacao { get; set; }

        public Pessoa Id { get; set; }
    }
}
