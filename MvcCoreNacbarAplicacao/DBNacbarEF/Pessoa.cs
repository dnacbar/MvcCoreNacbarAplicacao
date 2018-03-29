using System;
using System.Collections.Generic;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Pessoa
    {
        public Pessoa()
        {
            Agenda = new HashSet<Agenda>();
            Horario = new HashSet<Horario>();
            Pessoaemail = new HashSet<Pessoaemail>();
            Pessoaendereco = new HashSet<Pessoaendereco>();
            Pessoaocorrencia = new HashSet<Pessoaocorrencia>();
            Pessoatelefone = new HashSet<Pessoatelefone>();
            Profissional = new HashSet<Profissional>();
            Venda = new HashSet<Venda>();
        }

        public int IdEmpresa { get; set; }
        public int IdPessoa { get; set; }
        public string DsNomepessoa { get; set; }
        public string DsNomefantasia { get; set; }
        public bool BoTipopessoa { get; set; }
        public string DsCelular { get; set; }
        public string DsCelularresponsavel { get; set; }
        public string DsCep { get; set; }
        public string DsComplemento { get; set; }
        public string DsEmail { get; set; }
        public string DsInscricaoestadual { get; set; }
        public string DsInscricaofederal { get; set; }
        public string DsInscricaomunicipal { get; set; }
        public string DsLogradouro { get; set; }
        public string DsNumero { get; set; }
        public string DsObservacao { get; set; }
        public string DsResponsavel { get; set; }
        public string DsTelefone { get; set; }
        public string DsTelefoneresponsavel { get; set; }
        public DateTime DtAtualizacao { get; set; }
        public DateTime DtCadastro { get; set; }
        public DateTime? DtNascimento { get; set; }
        public int? IdBairro { get; set; }
        public int? IdBloqueio { get; set; }
        public short? IdEstadocivil { get; set; }
        public int? IdMunicipio { get; set; }

        public Bairro IdBairroNavigation { get; set; }
        public Motivobloqueio IdBloqueioNavigation { get; set; }
        public Empresa IdEmpresaNavigation { get; set; }
        public Estadocivil IdEstadocivilNavigation { get; set; }
        public Municipio IdMunicipioNavigation { get; set; }
        public ICollection<Agenda> Agenda { get; set; }
        public ICollection<Horario> Horario { get; set; }
        public ICollection<Pessoaemail> Pessoaemail { get; set; }
        public ICollection<Pessoaendereco> Pessoaendereco { get; set; }
        public ICollection<Pessoaocorrencia> Pessoaocorrencia { get; set; }
        public ICollection<Pessoatelefone> Pessoatelefone { get; set; }
        public ICollection<Profissional> Profissional { get; set; }
        public ICollection<Venda> Venda { get; set; }
    }
}
