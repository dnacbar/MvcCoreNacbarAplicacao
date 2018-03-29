using System;
using System.Collections.Generic;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Empresa
    {
        public Empresa()
        {
            Horario = new HashSet<Horario>();
            Pessoa = new HashSet<Pessoa>();
            Venda = new HashSet<Venda>();
        }

        public int IdEmpresa { get; set; }
        public string DsNomeempresa { get; set; }
        public string DsNomefantasia { get; set; }
        public string DsCep { get; set; }
        public string DsComplemento { get; set; }
        public string DsEmail { get; set; }
        public string DsInscricaoestadual { get; set; }
        public string DsInscricaofederal { get; set; }
        public string DsInscricaomunicipal { get; set; }
        public string DsLogradouro { get; set; }
        public string DsNumero { get; set; }
        public string DsTelefone { get; set; }
        public DateTime DtAtualizacao { get; set; }
        public DateTime DtCadastro { get; set; }
        public int? IdBairro { get; set; }
        public int? IdMunicipio { get; set; }
        public bool? BoAtiva { get; set; }
        public string DsImagem { get; set; }

        public Bairro IdBairroNavigation { get; set; }
        public Municipio IdMunicipioNavigation { get; set; }
        public ICollection<ApplicationUser> ApplicationUser { get; set; }
        public ICollection<Horario> Horario { get; set; }
        public ICollection<Pessoa> Pessoa { get; set; }
        public ICollection<Venda> Venda { get; set; }
    }
}
