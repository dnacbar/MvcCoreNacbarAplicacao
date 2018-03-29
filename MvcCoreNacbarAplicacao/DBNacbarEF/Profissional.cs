using System;
using System.Collections.Generic;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Profissional
    {
        public Profissional()
        {
            Agendaservico = new HashSet<Agendaservico>();
            Horario = new HashSet<Horario>();
        }

        public int IdEmpresa { get; set; }
        public int IdProfissional { get; set; }
        public int IdProfissao { get; set; }
        public int IdServico { get; set; }

        public Pessoa Id { get; set; }
        public Tipopessoa IdProfissaoNavigation { get; set; }
        public Servico IdServicoNavigation { get; set; }
        public ICollection<Agendaservico> Agendaservico { get; set; }
        public ICollection<Horario> Horario { get; set; }
    }
}
