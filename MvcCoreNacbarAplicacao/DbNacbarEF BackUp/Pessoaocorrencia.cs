using System;
using System.Collections.Generic;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Pessoaocorrencia
    {
        public int IdEmpresa { get; set; }
        public int IdPessoa { get; set; }
        public int IdOcorrencia { get; set; }
        public string DsOcorrencia { get; set; }
        public DateTime? DtOcorrencia { get; set; }

        public Pessoa Id { get; set; }
    }
}
