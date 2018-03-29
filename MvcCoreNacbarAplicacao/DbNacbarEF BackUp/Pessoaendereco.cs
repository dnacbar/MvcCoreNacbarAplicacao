using System;
using System.Collections.Generic;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Pessoaendereco
    {
        public int IdEmpresa { get; set; }
        public int IdPessoa { get; set; }
        public int IdEndereco { get; set; }
        public string DsCep { get; set; }
        public string DsComplemento { get; set; }
        public string DsLogradouro { get; set; }
        public string DsNumero { get; set; }
        public DateTime? DtAtualizacao { get; set; }
        public int? IdBairro { get; set; }
        public int? IdMunicipio { get; set; }

        public Pessoa Id { get; set; }
        public Bairro IdBairroNavigation { get; set; }
        public Municipio IdMunicipioNavigation { get; set; }
    }
}
