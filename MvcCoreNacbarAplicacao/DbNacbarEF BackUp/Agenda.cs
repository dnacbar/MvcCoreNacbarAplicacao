using System;
using System.Collections.Generic;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public partial class Agenda
    {
        public Agenda()
        {
            Agendaproduto = new HashSet<Agendaproduto>();
            Agendaservico = new HashSet<Agendaservico>();
            Financeiroagenda = new HashSet<Financeiroagenda>();
        }

        public int IdEmpresa { get; set; }
        public int IdAgenda { get; set; }
        public int IdCliente { get; set; }
        public DateTime DtAgendamento { get; set; }
        public TimeSpan HrAgendamento { get; set; }

        public Pessoa Id { get; set; }
        public ICollection<Agendaproduto> Agendaproduto { get; set; }
        public ICollection<Agendaservico> Agendaservico { get; set; }
        public ICollection<Financeiroagenda> Financeiroagenda { get; set; }
    }
}
