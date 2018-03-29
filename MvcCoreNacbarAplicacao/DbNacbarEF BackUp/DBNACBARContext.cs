using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MvcCoreNacbarAplicacao.DBNacbarEF
{
    public class ApplicationUser : IdentityUser
    {
        public int IntIdEmpresa { get; set; }
        public Empresa IdEmpresaNavigation { get; set; }
    }
    public partial class DBNACBARContext : IdentityDbContext<ApplicationUser>
    {
        public DBNACBARContext(DbContextOptions<DBNACBARContext> options) : base(options) { }
        public virtual DbSet<Agenda> Agenda { get; set; }
        public virtual DbSet<Agendaproduto> Agendaproduto { get; set; }
        public virtual DbSet<Agendaservico> Agendaservico { get; set; }
        public virtual DbSet<Bairro> Bairro { get; set; }
        public virtual DbSet<Cor> Cor { get; set; }
        public virtual DbSet<Empresa> Empresa { get; set; }
        public virtual DbSet<Estado> Estado { get; set; }
        public virtual DbSet<Estadocivil> Estadocivil { get; set; }
        public virtual DbSet<Financeiro> Financeiro { get; set; }
        public virtual DbSet<Financeiroagenda> Financeiroagenda { get; set; }
        public virtual DbSet<Financeirohorario> Financeirohorario { get; set; }
        public virtual DbSet<Formapagamento> Formapagamento { get; set; }
        public virtual DbSet<Horario> Horario { get; set; }
        public virtual DbSet<Marca> Marca { get; set; }
        public virtual DbSet<Motivobloqueio> Motivobloqueio { get; set; }
        public virtual DbSet<Municipio> Municipio { get; set; }
        public virtual DbSet<Pais> Pais { get; set; }
        public virtual DbSet<Pessoa> Pessoa { get; set; }
        public virtual DbSet<Pessoaemail> Pessoaemail { get; set; }
        public virtual DbSet<Pessoaendereco> Pessoaendereco { get; set; }
        public virtual DbSet<Pessoaocorrencia> Pessoaocorrencia { get; set; }
        public virtual DbSet<Pessoatelefone> Pessoatelefone { get; set; }
        public virtual DbSet<Produto> Produto { get; set; }
        public virtual DbSet<Profissional> Profissional { get; set; }
        public virtual DbSet<Servico> Servico { get; set; }
        public virtual DbSet<Tamanho> Tamanho { get; set; }
        public virtual DbSet<Tipopessoa> Tipopessoa { get; set; }
        public virtual DbSet<Tipoproduto> Tipoproduto { get; set; }
        public virtual DbSet<Unidade> Unidade { get; set; }
        public virtual DbSet<Venda> Venda { get; set; }
        public virtual DbSet<Vendaitem> Vendaitem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasIndex(e => e.IntIdEmpresa);

                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.IntIdEmpresa).HasColumnName("Id_Empresa");

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.ApplicationUser)
                    .HasForeignKey(d => d.IntIdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AspNetUsers_EMPRESA");
            });

            modelBuilder.Entity<Agenda>(entity =>
            {
                entity.HasKey(e => new { e.IdEmpresa, e.IdAgenda });

                entity.ToTable("AGENDA");

                entity.HasIndex(e => new { e.IdEmpresa, e.IdCliente });

                entity.Property(e => e.IdEmpresa).HasColumnName("ID_EMPRESA");

                entity.Property(e => e.IdAgenda).HasColumnName("ID_AGENDA");

                entity.Property(e => e.DtAgendamento)
                    .HasColumnName("DT_AGENDAMENTO")
                    .HasColumnType("date");

                entity.Property(e => e.HrAgendamento)
                    .HasColumnName("HR_AGENDAMENTO")
                    .HasColumnType("time(0)");

                entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.Agenda)
                    .HasForeignKey(d => new { d.IdEmpresa, d.IdCliente })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AGENDA_CLIENTE");
            });

            modelBuilder.Entity<Agendaproduto>(entity =>
            {
                entity.HasKey(e => new { e.IdEmpresa, e.IdAgenda, e.IdAgendaproduto });

                entity.ToTable("AGENDAPRODUTO");

                entity.HasIndex(e => new { e.IdEmpresa, e.IdProduto })
                    .HasName("IX_AGENDASERVICOPRODUTO_ID_EMPRESA_ID_PRODUTO");

                entity.Property(e => e.IdEmpresa).HasColumnName("ID_EMPRESA");

                entity.Property(e => e.IdAgenda).HasColumnName("ID_AGENDA");

                entity.Property(e => e.IdAgendaproduto).HasColumnName("ID_AGENDAPRODUTO");

                entity.Property(e => e.IdProduto).HasColumnName("ID_PRODUTO");

                entity.Property(e => e.NmQuantidade)
                    .HasColumnName("NM_QUANTIDADE")
                    .HasColumnType("numeric(28, 3)");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.Agendaproduto)
                    .HasForeignKey(d => new { d.IdEmpresa, d.IdAgenda })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AGENDAPRODUTO_AGENDA");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.Agendaproduto)
                    .HasForeignKey(d => new { d.IdEmpresa, d.IdProduto })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AGENDAPRODUTO_PRODUTO");
            });

            modelBuilder.Entity<Agendaservico>(entity =>
            {
                entity.HasKey(e => new { e.IdEmpresa, e.IdAgenda, e.IdAgendaservico });

                entity.ToTable("AGENDASERVICO");

                entity.HasIndex(e => new { e.IdEmpresa, e.IdProfissional, e.IdProfissao, e.IdServico });

                entity.Property(e => e.IdEmpresa).HasColumnName("ID_EMPRESA");

                entity.Property(e => e.IdAgenda).HasColumnName("ID_AGENDA");

                entity.Property(e => e.IdAgendaservico).HasColumnName("ID_AGENDASERVICO");

                entity.Property(e => e.IdProfissao).HasColumnName("ID_PROFISSAO");

                entity.Property(e => e.IdProfissional).HasColumnName("ID_PROFISSIONAL");

                entity.Property(e => e.IdServico).HasColumnName("ID_SERVICO");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.Agendaservico)
                    .HasForeignKey(d => new { d.IdEmpresa, d.IdAgenda })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AGENDASERVICO_AGENDA");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.Agendaservico)
                    .HasForeignKey(d => new { d.IdEmpresa, d.IdProfissional, d.IdProfissao, d.IdServico })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AGENDASERVICO_PROFISSIONAL");
            });

            modelBuilder.Entity<Bairro>(entity =>
            {
                entity.HasKey(e => e.IdBairro);

                entity.ToTable("BAIRRO");

                entity.Property(e => e.IdBairro)
                    .HasColumnName("ID_BAIRRO")
                    .ValueGeneratedNever();

                entity.Property(e => e.DsBairro)
                    .HasColumnName("DS_BAIRRO")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Cor>(entity =>
            {
                entity.HasKey(e => e.IdCor);

                entity.ToTable("COR");

                entity.Property(e => e.IdCor)
                    .HasColumnName("ID_COR")
                    .ValueGeneratedNever();

                entity.Property(e => e.DsCor)
                    .HasColumnName("DS_COR")
                    .HasMaxLength(60);
            });

            modelBuilder.Entity<Empresa>(entity =>
            {
                entity.HasKey(e => e.IdEmpresa);

                entity.ToTable("EMPRESA");

                entity.HasIndex(e => e.IdBairro);

                entity.HasIndex(e => e.IdMunicipio);

                entity.Property(e => e.IdEmpresa)
                    .HasColumnName("ID_EMPRESA")
                    .ValueGeneratedNever();

                entity.Property(e => e.BoAtiva)
                    .HasColumnName("BO_ATIVA")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.DsCep)
                    .HasColumnName("DS_CEP")
                    .HasMaxLength(8);

                entity.Property(e => e.DsComplemento)
                    .HasColumnName("DS_COMPLEMENTO")
                    .HasMaxLength(120);

                entity.Property(e => e.DsEmail)
                    .HasColumnName("DS_EMAIL")
                    .HasMaxLength(120);

                entity.Property(e => e.DsImagem)
                    .HasColumnName("DS_IMAGEM")
                    .HasMaxLength(50);

                entity.Property(e => e.DsInscricaoestadual)
                    .HasColumnName("DS_INSCRICAOESTADUAL")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DsInscricaofederal)
                    .HasColumnName("DS_INSCRICAOFEDERAL")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.DsInscricaomunicipal)
                    .HasColumnName("DS_INSCRICAOMUNICIPAL")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DsLogradouro)
                    .HasColumnName("DS_LOGRADOURO")
                    .HasMaxLength(200);

                entity.Property(e => e.DsNomeempresa)
                    .HasColumnName("DS_NOMEEMPRESA")
                    .HasMaxLength(200);

                entity.Property(e => e.DsNomefantasia)
                    .HasColumnName("DS_NOMEFANTASIA")
                    .HasMaxLength(200);

                entity.Property(e => e.DsNumero)
                    .HasColumnName("DS_NUMERO")
                    .HasMaxLength(60);

                entity.Property(e => e.DsTelefone)
                    .HasColumnName("DS_TELEFONE")
                    .HasMaxLength(11);

                entity.Property(e => e.DtAtualizacao)
                    .HasColumnName("DT_ATUALIZACAO")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.DtCadastro)
                    .HasColumnName("DT_CADASTRO")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.IdBairro).HasColumnName("ID_BAIRRO");

                entity.Property(e => e.IdMunicipio).HasColumnName("ID_MUNICIPIO");

                entity.HasOne(d => d.IdBairroNavigation)
                    .WithMany(p => p.Empresa)
                    .HasForeignKey(d => d.IdBairro)
                    .HasConstraintName("FK_EMPRESA_BAIRRO");

                entity.HasOne(d => d.IdMunicipioNavigation)
                    .WithMany(p => p.Empresa)
                    .HasForeignKey(d => d.IdMunicipio)
                    .HasConstraintName("FK_EMPRESA_MUNICIPIO");
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => new { e.IdPais, e.IdEstado });

                entity.ToTable("ESTADO");

                entity.Property(e => e.IdPais)
                    .HasColumnName("ID_PAIS")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.IdEstado).HasColumnName("ID_ESTADO");

                entity.Property(e => e.DsEstado)
                    .HasColumnName("DS_ESTADO")
                    .HasMaxLength(200);

                entity.Property(e => e.DsUf)
                    .HasColumnName("DS_UF")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPaisNavigation)
                    .WithMany(p => p.Estado)
                    .HasForeignKey(d => d.IdPais)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ESTADO_PAIS");
            });

            modelBuilder.Entity<Estadocivil>(entity =>
            {
                entity.HasKey(e => e.IdEstadocivil);

                entity.ToTable("ESTADOCIVIL");

                entity.Property(e => e.IdEstadocivil)
                    .HasColumnName("ID_ESTADOCIVIL")
                    .ValueGeneratedNever();

                entity.Property(e => e.DsEstadocivil)
                    .HasColumnName("DS_ESTADOCIVIL")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Financeiro>(entity =>
            {
                entity.HasKey(e => new { e.IdEmpresa, e.IdFinanceiro });

                entity.ToTable("FINANCEIRO");

                entity.HasIndex(e => e.IdFormapagamento);

                entity.Property(e => e.IdEmpresa).HasColumnName("ID_EMPRESA");

                entity.Property(e => e.IdFinanceiro).HasColumnName("ID_FINANCEIRO");

                entity.Property(e => e.BoOperacao).HasColumnName("BO_OPERACAO");

                entity.Property(e => e.CdSituacao).HasColumnName("CD_SITUACAO");

                entity.Property(e => e.DsHistorico).HasColumnName("DS_HISTORICO");

                entity.Property(e => e.DtCriacao)
                    .HasColumnName("DT_CRIACAO")
                    .HasColumnType("date");

                entity.Property(e => e.DtPagamento)
                    .HasColumnName("DT_PAGAMENTO")
                    .HasColumnType("date");

                entity.Property(e => e.DtVencimento)
                    .HasColumnName("DT_VENCIMENTO")
                    .HasColumnType("date");

                entity.Property(e => e.IdFormapagamento).HasColumnName("ID_FORMAPAGAMENTO");

                entity.Property(e => e.NmDesconto)
                    .HasColumnName("NM_DESCONTO")
                    .HasColumnType("numeric(28, 2)");

                entity.Property(e => e.NmJuros)
                    .HasColumnName("NM_JUROS")
                    .HasColumnType("numeric(28, 2)");

                entity.Property(e => e.NmSaldo)
                    .HasColumnName("NM_SALDO")
                    .HasColumnType("numeric(28, 2)");

                entity.Property(e => e.NmValor)
                    .HasColumnName("NM_VALOR")
                    .HasColumnType("numeric(28, 2)");

                entity.Property(e => e.NmValorpago)
                    .HasColumnName("NM_VALORPAGO")
                    .HasColumnType("numeric(28, 2)");

                entity.HasOne(d => d.IdFormapagamentoNavigation)
                    .WithMany(p => p.Financeiro)
                    .HasForeignKey(d => d.IdFormapagamento)
                    .HasConstraintName("FK_FINANCEIRO_FORMAPAGAMENTO");
            });

            modelBuilder.Entity<Financeiroagenda>(entity =>
            {
                entity.HasKey(e => new { e.IdEmpresa, e.IdFinanceiro, e.IdAgenda });

                entity.ToTable("FINANCEIROAGENDA");

                entity.HasIndex(e => new { e.IdEmpresa, e.IdAgenda });

                entity.Property(e => e.IdEmpresa).HasColumnName("ID_EMPRESA");

                entity.Property(e => e.IdFinanceiro).HasColumnName("ID_FINANCEIRO");

                entity.Property(e => e.IdAgenda).HasColumnName("ID_AGENDA");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.Financeiroagenda)
                    .HasForeignKey(d => new { d.IdEmpresa, d.IdAgenda })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FINANCEIROAGENDA_AGENDA");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.Financeiroagenda)
                    .HasForeignKey(d => new { d.IdEmpresa, d.IdFinanceiro })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FINANCEIROAGENDA_FINANCEIRO");
            });

            modelBuilder.Entity<Financeirohorario>(entity =>
            {
                entity.HasKey(e => new { e.IdEmpresa, e.IdFinanceiro, e.IdCliente, e.IdProfissional, e.IdProfissao, e.IdServico });

                entity.ToTable("FINANCEIROHORARIO");

                entity.Property(e => e.IdEmpresa).HasColumnName("ID_EMPRESA");

                entity.Property(e => e.IdFinanceiro).HasColumnName("ID_FINANCEIRO");

                entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");

                entity.Property(e => e.IdProfissional).HasColumnName("ID_PROFISSIONAL");

                entity.Property(e => e.IdProfissao).HasColumnName("ID_PROFISSAO");

                entity.Property(e => e.IdServico).HasColumnName("ID_SERVICO");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.Financeirohorario)
                    .HasForeignKey(d => new { d.IdEmpresa, d.IdFinanceiro })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FINANCEIROHORARIO_FINANCEIRO");
            });

            modelBuilder.Entity<Formapagamento>(entity =>
            {
                entity.HasKey(e => e.IdFormapagamento);

                entity.ToTable("FORMAPAGAMENTO");

                entity.Property(e => e.IdFormapagamento)
                    .HasColumnName("ID_FORMAPAGAMENTO")
                    .ValueGeneratedNever();

                entity.Property(e => e.DsFormapagamento)
                    .HasColumnName("DS_FORMAPAGAMENTO")
                    .HasMaxLength(60);
            });

            modelBuilder.Entity<Horario>(entity =>
            {
                entity.HasKey(e => new { e.IdEmpresa, e.IdHorario });

                entity.ToTable("HORARIO");

                entity.HasIndex(e => new { e.IdEmpresa, e.IdCliente });

                entity.HasIndex(e => new { e.IdEmpresa, e.IdProfissional, e.IdProfissao, e.IdServico });

                entity.Property(e => e.IdEmpresa).HasColumnName("ID_EMPRESA");

                entity.Property(e => e.IdHorario).HasColumnName("ID_HORARIO");

                entity.Property(e => e.CdDiasemana)
                    .IsRequired()
                    .HasColumnName("CD_DIASEMANA")
                    .HasMaxLength(13);

                entity.Property(e => e.HrHorariofinal)
                    .HasColumnName("HR_HORARIOFINAL")
                    .HasColumnType("time(0)");

                entity.Property(e => e.HrHorarioinicial)
                    .HasColumnName("HR_HORARIOINICIAL")
                    .HasColumnType("time(0)");

                entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");

                entity.Property(e => e.IdProfissao).HasColumnName("ID_PROFISSAO");

                entity.Property(e => e.IdProfissional).HasColumnName("ID_PROFISSIONAL");

                entity.Property(e => e.IdServico).HasColumnName("ID_SERVICO");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Horario)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HORARIO_EMPRESA");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.Horario)
                    .HasForeignKey(d => new { d.IdEmpresa, d.IdCliente })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HORARIO_CLIENTE");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.Horario)
                    .HasForeignKey(d => new { d.IdEmpresa, d.IdProfissional, d.IdProfissao, d.IdServico })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_HORARIO_PROFISSIONAL");
            });

            modelBuilder.Entity<Marca>(entity =>
            {
                entity.HasKey(e => e.IdMarca);

                entity.ToTable("MARCA");

                entity.Property(e => e.IdMarca)
                    .HasColumnName("ID_MARCA")
                    .ValueGeneratedNever();

                entity.Property(e => e.DsMarca)
                    .HasColumnName("DS_MARCA")
                    .HasMaxLength(120);
            });

            modelBuilder.Entity<Motivobloqueio>(entity =>
            {
                entity.HasKey(e => e.IdBloqueio);

                entity.ToTable("MOTIVOBLOQUEIO");

                entity.Property(e => e.IdBloqueio)
                    .HasColumnName("ID_BLOQUEIO")
                    .ValueGeneratedNever();

                entity.Property(e => e.DsMotivobloqueio)
                    .HasColumnName("DS_MOTIVOBLOQUEIO")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Municipio>(entity =>
            {
                entity.HasKey(e => e.IdMunicipio);

                entity.ToTable("MUNICIPIO");

                entity.Property(e => e.IdMunicipio)
                    .HasColumnName("ID_MUNICIPIO")
                    .ValueGeneratedNever();

                entity.Property(e => e.CdMunicipio)
                    .HasColumnName("CD_MUNICIPIO")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.DsMunicipio)
                    .HasColumnName("DS_MUNICIPIO")
                    .HasMaxLength(200);

                entity.Property(e => e.IdEstado).HasColumnName("ID_ESTADO");

                entity.Property(e => e.IdPais)
                    .IsRequired()
                    .HasColumnName("ID_PAIS")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.Municipio)
                    .HasForeignKey(d => new { d.IdPais, d.IdEstado })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MUNICIPIO_ESTADO");
            });

            modelBuilder.Entity<Pais>(entity =>
            {
                entity.HasKey(e => e.IdPais);

                entity.ToTable("PAIS");

                entity.Property(e => e.IdPais)
                    .HasColumnName("ID_PAIS")
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DsPais)
                    .HasColumnName("DS_PAIS")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Pessoa>(entity =>
            {
                entity.HasKey(e => new { e.IdEmpresa, e.IdPessoa });

                entity.ToTable("PESSOA");

                entity.HasIndex(e => e.IdBairro);

                entity.HasIndex(e => e.IdBloqueio);

                entity.HasIndex(e => e.IdEstadocivil);

                entity.HasIndex(e => e.IdMunicipio);

                entity.Property(e => e.IdEmpresa).HasColumnName("ID_EMPRESA");

                entity.Property(e => e.IdPessoa).HasColumnName("ID_PESSOA");

                entity.Property(e => e.BoTipopessoa).HasColumnName("BO_TIPOPESSOA");

                entity.Property(e => e.DsCelular)
                    .HasColumnName("DS_CELULAR")
                    .HasMaxLength(11);

                entity.Property(e => e.DsCelularresponsavel)
                    .HasColumnName("DS_CELULARRESPONSAVEL")
                    .HasMaxLength(11);

                entity.Property(e => e.DsCep)
                    .HasColumnName("DS_CEP")
                    .HasMaxLength(8);

                entity.Property(e => e.DsComplemento)
                    .HasColumnName("DS_COMPLEMENTO")
                    .HasMaxLength(120);

                entity.Property(e => e.DsEmail)
                    .HasColumnName("DS_EMAIL")
                    .HasMaxLength(120);

                entity.Property(e => e.DsInscricaoestadual)
                    .HasColumnName("DS_INSCRICAOESTADUAL")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DsInscricaofederal)
                    .HasColumnName("DS_INSCRICAOFEDERAL")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.DsInscricaomunicipal)
                    .HasColumnName("DS_INSCRICAOMUNICIPAL")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DsLogradouro)
                    .HasColumnName("DS_LOGRADOURO")
                    .HasMaxLength(200);

                entity.Property(e => e.DsNomefantasia)
                    .HasColumnName("DS_NOMEFANTASIA")
                    .HasMaxLength(200);

                entity.Property(e => e.DsNomepessoa)
                    .HasColumnName("DS_NOMEPESSOA")
                    .HasMaxLength(200);

                entity.Property(e => e.DsNumero)
                    .HasColumnName("DS_NUMERO")
                    .HasMaxLength(60);

                entity.Property(e => e.DsObservacao).HasColumnName("DS_OBSERVACAO");

                entity.Property(e => e.DsResponsavel)
                    .HasColumnName("DS_RESPONSAVEL")
                    .HasMaxLength(200);

                entity.Property(e => e.DsTelefone)
                    .HasColumnName("DS_TELEFONE")
                    .HasMaxLength(11);

                entity.Property(e => e.DsTelefoneresponsavel)
                    .HasColumnName("DS_TELEFONERESPONSAVEL")
                    .HasMaxLength(11);

                entity.Property(e => e.DtAtualizacao)
                    .HasColumnName("DT_ATUALIZACAO")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.DtCadastro)
                    .HasColumnName("DT_CADASTRO")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.DtNascimento)
                    .HasColumnName("DT_NASCIMENTO")
                    .HasColumnType("date");

                entity.Property(e => e.IdBairro).HasColumnName("ID_BAIRRO");

                entity.Property(e => e.IdBloqueio).HasColumnName("ID_BLOQUEIO");

                entity.Property(e => e.IdEstadocivil).HasColumnName("ID_ESTADOCIVIL");

                entity.Property(e => e.IdMunicipio).HasColumnName("ID_MUNICIPIO");

                entity.HasOne(d => d.IdBairroNavigation)
                    .WithMany(p => p.Pessoa)
                    .HasForeignKey(d => d.IdBairro)
                    .HasConstraintName("FK_PESSOA_BAIRRO");

                entity.HasOne(d => d.IdBloqueioNavigation)
                    .WithMany(p => p.Pessoa)
                    .HasForeignKey(d => d.IdBloqueio)
                    .HasConstraintName("FK_PESSOA_MOTIVOBLOQUEIO");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Pessoa)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PESSOA_EMPRESA");

                entity.HasOne(d => d.IdEstadocivilNavigation)
                    .WithMany(p => p.Pessoa)
                    .HasForeignKey(d => d.IdEstadocivil)
                    .HasConstraintName("FK_PESSOA_ESTADOCIVIL");

                entity.HasOne(d => d.IdMunicipioNavigation)
                    .WithMany(p => p.Pessoa)
                    .HasForeignKey(d => d.IdMunicipio)
                    .HasConstraintName("FK_PESSOA_MUNICIPIO");
            });

            modelBuilder.Entity<Pessoaemail>(entity =>
            {
                entity.HasKey(e => new { e.IdEmpresa, e.IdPessoa, e.IdEmail });

                entity.ToTable("PESSOAEMAIL");

                entity.Property(e => e.IdEmpresa).HasColumnName("ID_EMPRESA");

                entity.Property(e => e.IdPessoa).HasColumnName("ID_PESSOA");

                entity.Property(e => e.IdEmail).HasColumnName("ID_EMAIL");

                entity.Property(e => e.DsContato)
                    .HasColumnName("DS_CONTATO")
                    .HasMaxLength(120);

                entity.Property(e => e.DsEmail)
                    .HasColumnName("DS_EMAIL")
                    .HasMaxLength(120);

                entity.Property(e => e.DtAtualizacao)
                    .HasColumnName("DT_ATUALIZACAO")
                    .HasColumnType("datetime2(0)");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.Pessoaemail)
                    .HasForeignKey(d => new { d.IdEmpresa, d.IdPessoa })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PESSOAEMAIL_PESSOA");
            });

            modelBuilder.Entity<Pessoaendereco>(entity =>
            {
                entity.HasKey(e => new { e.IdEmpresa, e.IdPessoa, e.IdEndereco });

                entity.ToTable("PESSOAENDERECO");

                entity.HasIndex(e => e.IdBairro);

                entity.HasIndex(e => e.IdMunicipio);

                entity.Property(e => e.IdEmpresa).HasColumnName("ID_EMPRESA");

                entity.Property(e => e.IdPessoa).HasColumnName("ID_PESSOA");

                entity.Property(e => e.IdEndereco).HasColumnName("ID_ENDERECO");

                entity.Property(e => e.DsCep)
                    .HasColumnName("DS_CEP")
                    .HasMaxLength(8);

                entity.Property(e => e.DsComplemento)
                    .HasColumnName("DS_COMPLEMENTO")
                    .HasMaxLength(120);

                entity.Property(e => e.DsLogradouro)
                    .HasColumnName("DS_LOGRADOURO")
                    .HasMaxLength(200);

                entity.Property(e => e.DsNumero)
                    .HasColumnName("DS_NUMERO")
                    .HasMaxLength(60);

                entity.Property(e => e.DtAtualizacao)
                    .HasColumnName("DT_ATUALIZACAO")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.IdBairro).HasColumnName("ID_BAIRRO");

                entity.Property(e => e.IdMunicipio).HasColumnName("ID_MUNICIPIO");

                entity.HasOne(d => d.IdBairroNavigation)
                    .WithMany(p => p.Pessoaendereco)
                    .HasForeignKey(d => d.IdBairro)
                    .HasConstraintName("FK_PESSOAENDERECO_BAIRRO");

                entity.HasOne(d => d.IdMunicipioNavigation)
                    .WithMany(p => p.Pessoaendereco)
                    .HasForeignKey(d => d.IdMunicipio)
                    .HasConstraintName("FK_PESSOAENDERECO_MUNICIPIO");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.Pessoaendereco)
                    .HasForeignKey(d => new { d.IdEmpresa, d.IdPessoa })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PESSOAENDERECO_PESSOA");
            });

            modelBuilder.Entity<Pessoaocorrencia>(entity =>
            {
                entity.HasKey(e => new { e.IdEmpresa, e.IdPessoa, e.IdOcorrencia });

                entity.ToTable("PESSOAOCORRENCIA");

                entity.Property(e => e.IdEmpresa).HasColumnName("ID_EMPRESA");

                entity.Property(e => e.IdPessoa).HasColumnName("ID_PESSOA");

                entity.Property(e => e.IdOcorrencia).HasColumnName("ID_OCORRENCIA");

                entity.Property(e => e.DsOcorrencia).HasColumnName("DS_OCORRENCIA");

                entity.Property(e => e.DtOcorrencia)
                    .HasColumnName("DT_OCORRENCIA")
                    .HasColumnType("datetime2(0)");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.Pessoaocorrencia)
                    .HasForeignKey(d => new { d.IdEmpresa, d.IdPessoa })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PESSOAOCORRENCIA_PESSOA");
            });

            modelBuilder.Entity<Pessoatelefone>(entity =>
            {
                entity.HasKey(e => new { e.IdEmpresa, e.IdPessoa, e.IdTelefone });

                entity.ToTable("PESSOATELEFONE");

                entity.Property(e => e.IdEmpresa).HasColumnName("ID_EMPRESA");

                entity.Property(e => e.IdPessoa).HasColumnName("ID_PESSOA");

                entity.Property(e => e.IdTelefone).HasColumnName("ID_TELEFONE");

                entity.Property(e => e.DsContato)
                    .HasColumnName("DS_CONTATO")
                    .HasMaxLength(120);

                entity.Property(e => e.DsTelefone)
                    .HasColumnName("DS_TELEFONE")
                    .HasMaxLength(11);

                entity.Property(e => e.DtAtualizacao)
                    .HasColumnName("DT_ATUALIZACAO")
                    .HasColumnType("datetime2(0)");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.Pessoatelefone)
                    .HasForeignKey(d => new { d.IdEmpresa, d.IdPessoa })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PESSOATELEFONE_PESSOA");
            });

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(e => new { e.IdEmpresa, e.IdProduto });

                entity.ToTable("PRODUTO");

                entity.HasIndex(e => e.IdBloqueio);

                entity.HasIndex(e => e.IdCor);

                entity.HasIndex(e => e.IdMarca);

                entity.HasIndex(e => e.IdTamanho);

                entity.HasIndex(e => e.IdTipoproduto);

                entity.HasIndex(e => e.IdUnidade);

                entity.Property(e => e.IdEmpresa).HasColumnName("ID_EMPRESA");

                entity.Property(e => e.IdProduto).HasColumnName("ID_PRODUTO");

                entity.Property(e => e.DsNomeproduto)
                    .HasColumnName("DS_DESCPRODUTO")
                    .HasMaxLength(120);

                entity.Property(e => e.DsDesctecnica).HasColumnName("DS_DESCTECNICA");

                entity.Property(e => e.DsEan)
                    .HasColumnName("DS_EAN")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DsObservacao).HasColumnName("DS_OBSERVACAO");

                entity.Property(e => e.DtAtualizacao)
                    .HasColumnName("DT_ATUALIZACAO")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.DtCadastro)
                    .HasColumnName("DT_CADASTRO")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.IdBloqueio).HasColumnName("ID_BLOQUEIO");

                entity.Property(e => e.IdCor).HasColumnName("ID_COR");

                entity.Property(e => e.IdMarca).HasColumnName("ID_MARCA");

                entity.Property(e => e.IdTamanho).HasColumnName("ID_TAMANHO");

                entity.Property(e => e.IdTipoproduto).HasColumnName("ID_TIPOPRODUTO");

                entity.Property(e => e.IdUnidade).HasColumnName("ID_UNIDADE");

                entity.Property(e => e.NmEstoque).HasColumnName("NM_ESTOQUE");

                entity.Property(e => e.NmMinimo).HasColumnName("NM_MINIMO");

                entity.Property(e => e.NmQtdeabsoluta)
                    .HasColumnName("NM_QTDEABSOLUTA")
                    .HasColumnType("numeric(10, 3)");

                entity.Property(e => e.NmQtderelativa).HasColumnName("NM_QTDERELATIVA");

                entity.Property(e => e.NmUnidade)
                    .HasColumnName("NM_UNIDADE")
                    .HasColumnType("numeric(7, 3)");

                entity.Property(e => e.NmValorcompra)
                    .HasColumnName("NM_VALORCOMPRA")
                    .HasColumnType("numeric(28, 4)");

                entity.Property(e => e.NmValorvenda)
                    .HasColumnName("NM_VALORVENDA")
                    .HasColumnType("numeric(28, 4)");

                entity.HasOne(d => d.IdBloqueioNavigation)
                    .WithMany(p => p.Produto)
                    .HasForeignKey(d => d.IdBloqueio)
                    .HasConstraintName("FK_PRODUTO_BLOQUEIO");

                entity.HasOne(d => d.IdCorNavigation)
                    .WithMany(p => p.Produto)
                    .HasForeignKey(d => d.IdCor)
                    .HasConstraintName("FK_PRODUTO_COR");

                entity.HasOne(d => d.IdMarcaNavigation)
                    .WithMany(p => p.Produto)
                    .HasForeignKey(d => d.IdMarca)
                    .HasConstraintName("FK_PRODUTO_MARCA");

                entity.HasOne(d => d.IdTamanhoNavigation)
                    .WithMany(p => p.Produto)
                    .HasForeignKey(d => d.IdTamanho)
                    .HasConstraintName("FK_PRODUTO_TAMANHO");

                entity.HasOne(d => d.IdTipoprodutoNavigation)
                    .WithMany(p => p.Produto)
                    .HasForeignKey(d => d.IdTipoproduto)
                    .HasConstraintName("FK_PRODUTO_TIPOPRODUTO");

                entity.HasOne(d => d.IdUnidadeNavigation)
                    .WithMany(p => p.Produto)
                    .HasForeignKey(d => d.IdUnidade)
                    .HasConstraintName("FK_PRODUTO_UNIDADE");
            });

            modelBuilder.Entity<Profissional>(entity =>
            {
                entity.HasKey(e => new { e.IdEmpresa, e.IdProfissional, e.IdProfissao, e.IdServico });

                entity.ToTable("PROFISSIONAL");

                entity.HasIndex(e => e.IdProfissao);

                entity.HasIndex(e => e.IdServico);

                entity.Property(e => e.IdEmpresa).HasColumnName("ID_EMPRESA");

                entity.Property(e => e.IdProfissional).HasColumnName("ID_PROFISSIONAL");

                entity.Property(e => e.IdProfissao).HasColumnName("ID_PROFISSAO");

                entity.Property(e => e.IdServico).HasColumnName("ID_SERVICO");

                entity.HasOne(d => d.IdProfissaoNavigation)
                    .WithMany(p => p.Profissional)
                    .HasForeignKey(d => d.IdProfissao)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PROFISSIONAL_TIPOPESSOA");

                entity.HasOne(d => d.IdServicoNavigation)
                    .WithMany(p => p.Profissional)
                    .HasForeignKey(d => d.IdServico)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PROFISSIONAL_SERVICO");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.Profissional)
                    .HasForeignKey(d => new { d.IdEmpresa, d.IdProfissional })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PROFISSIONAL_PESSOA");
            });

            modelBuilder.Entity<Servico>(entity =>
            {
                entity.HasKey(e => e.IdServico);

                entity.ToTable("SERVICO");

                entity.Property(e => e.IdServico)
                    .HasColumnName("ID_SERVICO")
                    .ValueGeneratedNever();

                entity.Property(e => e.DsServico)
                    .HasColumnName("DS_SERVICO")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.NmValor)
                    .HasColumnName("NM_VALOR")
                    .HasColumnType("numeric(25, 10)");
            });

            modelBuilder.Entity<Tamanho>(entity =>
            {
                entity.HasKey(e => e.IdTamanho);

                entity.ToTable("TAMANHO");

                entity.Property(e => e.IdTamanho)
                    .HasColumnName("ID_TAMANHO")
                    .ValueGeneratedNever();

                entity.Property(e => e.DsTamanho)
                    .HasColumnName("DS_TAMANHO")
                    .HasMaxLength(60);
            });

            modelBuilder.Entity<Tipopessoa>(entity =>
            {
                entity.HasKey(e => e.IdTipopessoa);

                entity.ToTable("TIPOPESSOA");

                entity.Property(e => e.IdTipopessoa)
                    .HasColumnName("ID_TIPOPESSOA")
                    .ValueGeneratedNever();

                entity.Property(e => e.DsTipopessoa)
                    .HasColumnName("DS_TIPOPESSOA")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Tipoproduto>(entity =>
            {
                entity.HasKey(e => e.IdTipoproduto);

                entity.ToTable("TIPOPRODUTO");

                entity.Property(e => e.IdTipoproduto)
                    .HasColumnName("ID_TIPOPRODUTO")
                    .ValueGeneratedNever();

                entity.Property(e => e.DsTipoproduto)
                    .HasColumnName("DS_TIPOPRODUTO")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Unidade>(entity =>
            {
                entity.HasKey(e => e.IdUnidade);

                entity.ToTable("UNIDADE");

                entity.Property(e => e.IdUnidade)
                    .HasColumnName("ID_UNIDADE")
                    .ValueGeneratedNever();

                entity.Property(e => e.DsUnidade)
                    .HasColumnName("DS_UNIDADE")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Venda>(entity =>
            {
                entity.HasKey(e => new { e.IdEmpresa, e.IdVenda });

                entity.ToTable("VENDA");

                entity.HasIndex(e => new { e.IdEmpresa, e.IdCliente });

                entity.Property(e => e.IdEmpresa).HasColumnName("ID_EMPRESA");

                entity.Property(e => e.IdVenda).HasColumnName("ID_VENDA");

                entity.Property(e => e.DtCancelamento)
                    .HasColumnName("DT_CANCELAMENTO")
                    .HasColumnType("date");

                entity.Property(e => e.DtDataconclusao)
                    .HasColumnName("DT_DATACONCLUSAO")
                    .HasColumnType("date");

                entity.Property(e => e.DtDatavenda)
                    .HasColumnName("DT_DATAVENDA")
                    .HasColumnType("date");

                entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");

                entity.Property(e => e.NmAcrescimo)
                    .HasColumnName("NM_ACRESCIMO")
                    .HasColumnType("numeric(28, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.NmDesconto)
                    .HasColumnName("NM_DESCONTO")
                    .HasColumnType("numeric(28, 2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.NmFrete)
                    .HasColumnName("NM_FRETE")
                    .HasColumnType("numeric(28, 2)");

                entity.Property(e => e.NmSubtotal)
                    .HasColumnName("NM_SUBTOTAL")
                    .HasColumnType("numeric(28, 2)");

                entity.Property(e => e.NmTotal)
                    .HasColumnName("NM_TOTAL")
                    .HasColumnType("numeric(28, 2)");

                entity.HasOne(d => d.IdEmpresaNavigation)
                    .WithMany(p => p.Venda)
                    .HasForeignKey(d => d.IdEmpresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VENDAS_EMPRESA");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.Venda)
                    .HasForeignKey(d => new { d.IdEmpresa, d.IdCliente })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VENDAS_CLIENTE");
            });

            modelBuilder.Entity<Vendaitem>(entity =>
            {
                entity.HasKey(e => new { e.IdEmpresa, e.IdVenda, e.IdVendaitem });

                entity.ToTable("VENDAITEM");

                entity.HasIndex(e => new { e.IdEmpresa, e.IdProduto });

                entity.Property(e => e.IdEmpresa).HasColumnName("ID_EMPRESA");

                entity.Property(e => e.IdVenda).HasColumnName("ID_VENDA");

                entity.Property(e => e.IdVendaitem).HasColumnName("ID_VENDAITEM");

                entity.Property(e => e.IdProduto).HasColumnName("ID_PRODUTO");

                entity.Property(e => e.NmAcrescimounitario)
                    .HasColumnName("NM_ACRESCIMOUNITARIO")
                    .HasColumnType("numeric(28, 10)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.NmDescontounitario)
                    .HasColumnName("NM_DESCONTOUNITARIO")
                    .HasColumnType("numeric(28, 10)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.NmQuantidade)
                    .HasColumnName("NM_QUANTIDADE")
                    .HasColumnType("numeric(28, 3)");

                entity.Property(e => e.NmQuantidadedevolvida)
                    .HasColumnName("NM_QUANTIDADEDEVOLVIDA")
                    .HasColumnType("numeric(28, 3)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.NmTotal)
                    .HasColumnName("NM_TOTAL")
                    .HasColumnType("numeric(28, 2)");

                entity.Property(e => e.NmValorfinalunitario)
                    .HasColumnName("NM_VALORFINALUNITARIO")
                    .HasColumnType("numeric(28, 10)");

                entity.Property(e => e.NmValorunitario)
                    .HasColumnName("NM_VALORUNITARIO")
                    .HasColumnType("numeric(28, 10)");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.Vendaitem)
                    .HasForeignKey(d => new { d.IdEmpresa, d.IdProduto })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VENDAITEM_PRODUTO");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.Vendaitem)
                    .HasForeignKey(d => new { d.IdEmpresa, d.IdVenda })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_VENDAITEM_VENDA");
            });
        }
    }
}
