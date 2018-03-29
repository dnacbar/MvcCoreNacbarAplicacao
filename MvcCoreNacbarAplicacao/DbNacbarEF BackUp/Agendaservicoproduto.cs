namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Agendaservicoproduto
    {
        public int IdEmpresa { get; set; }
        public int IdAgenda { get; set; }
        public int IdAgendaservico { get; set; }
        public int IdAgendaservicoproduto { get; set; }
        public int IdProduto { get; set; }
        public decimal NmQuantidade { get; set; }

        public Produto Id { get; set; }
        public Agendaservico IdNavigation { get; set; }
    }
}
