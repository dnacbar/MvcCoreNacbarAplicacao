using System;
using System.Collections.Generic;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Produto
    {
        public Produto()
        {
            Agendaproduto = new HashSet<Agendaproduto>();
            Vendaitem = new HashSet<Vendaitem>();
        }

        public int IdEmpresa { get; set; }
        public int IdProduto { get; set; }
        public string DsNomeproduto { get; set; }
        public string DsDesctecnica { get; set; }
        public string DsEan { get; set; }
        public string DsObservacao { get; set; }
        public DateTime DtAtualizacao { get; set; }
        public DateTime DtCadastro { get; set; }
        public int? IdBloqueio { get; set; }
        public int? IdCor { get; set; }
        public int? IdMarca { get; set; }
        public int? IdTamanho { get; set; }
        public int? IdTipoproduto { get; set; }
        public int? IdUnidade { get; set; }
        public int? NmEstoque { get; set; }
        public int? NmMinimo { get; set; }
        public decimal? NmQtdeabsoluta { get; set; }
        public int? NmQtderelativa { get; set; }
        public decimal? NmUnidade { get; set; }
        public decimal? NmValorcompra { get; set; }
        public decimal? NmValorvenda { get; set; }

        public Motivobloqueio IdBloqueioNavigation { get; set; }
        public Cor IdCorNavigation { get; set; }
        public Marca IdMarcaNavigation { get; set; }
        public Tamanho IdTamanhoNavigation { get; set; }
        public Tipoproduto IdTipoprodutoNavigation { get; set; }
        public Unidade IdUnidadeNavigation { get; set; }
        public ICollection<Agendaproduto> Agendaproduto { get; set; }
        public ICollection<Vendaitem> Vendaitem { get; set; }
    }
}
