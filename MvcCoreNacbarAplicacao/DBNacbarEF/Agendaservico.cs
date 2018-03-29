using System;
using System.Collections.Generic;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Agendaservico
    {
        public int IdEmpresa { get; set; }
        public int IdAgenda { get; set; }
        public int IdAgendaservico { get; set; }
        public int IdProfissional { get; set; }
        public int IdProfissao { get; set; }
        public int IdServico { get; set; }

        public Agenda Id { get; set; }
        public Profissional IdNavigation { get; set; }
    }
}
