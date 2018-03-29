using System;
using System.Collections.Generic;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Horario
    {
        public int IdEmpresa { get; set; }
        public int IdHorario { get; set; }
        public string CdDiasemana { get; set; }
        public TimeSpan HrHorariofinal { get; set; }
        public TimeSpan HrHorarioinicial { get; set; }
        public int IdCliente { get; set; }
        public int IdProfissao { get; set; }
        public int IdProfissional { get; set; }
        public int IdServico { get; set; }

        public Pessoa Id { get; set; }
        public Empresa IdEmpresaNavigation { get; set; }
        public Profissional IdNavigation { get; set; }
    }
}
