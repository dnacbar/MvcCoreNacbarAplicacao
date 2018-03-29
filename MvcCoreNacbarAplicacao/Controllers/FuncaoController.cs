using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcCoreNacbarAplicacao.DBNacbarEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreNacbarAplicacao.Controllers
{
    public class FuncaoController : Controller
    {
        public DBNACBARContext DBNacbar;
        BuscaCnpj buscaCnpj = null;

        public FuncaoController(DBNACBARContext context)
        {
            DBNacbar = context;
        }

        public async Task<IList<Agenda>> GetAgendaAsync(DateTime dateAgenda)
        {
            return await DBNacbar.Agenda
                                 .Select(p => new Agenda
                                 {
                                     IdEmpresa = p.IdEmpresa,
                                     IdAgenda = p.IdAgenda,
                                     Id = new Pessoa
                                     {
                                         DsNomepessoa = p.Id.DsNomepessoa,
                                     },
                                     HrAgendamento = p.HrAgendamento,
                                     DtAgendamento = p.DtAgendamento
                                 }).Where(p => /*p.IdEmpresa == NacbarAplicacao.IdEmpresa &&*/ p.DtAgendamento == dateAgenda)
                                 .OrderBy(p => p.HrAgendamento)
                                 .AsNoTracking().ToListAsync();
        }

        public async Task<IList<Agendaproduto>> GetAgendaProdutoAsync(int intAgenda)
        {
            return await DBNacbar.Agendaproduto
                                 .Select(p => new Agendaproduto
                                 {
                                     IdEmpresa = p.IdEmpresa,
                                     IdAgenda = p.IdAgenda,
                                     IdAgendaproduto = p.IdAgendaproduto,
                                     IdNavigation = new Produto
                                     {
                                         DsNomeproduto = p.IdNavigation.DsNomeproduto
                                     },
                                     NmQuantidade = p.NmQuantidade
                                 }).Where(p => /*p.IdEmpresa == NacbarAplicacao.IdEmpresa &&*/ p.IdAgenda == intAgenda)
                                 //.OrderBy(p => p.Id.DsDescproduto)
                                 .AsNoTracking().ToListAsync();
        }

        public async Task<IList<Agendaservico>> GetAgendaServicoAsync(int intAgenda)
        {
            return await DBNacbar.Agendaservico
                                 .Select(p => new Agendaservico
                                 {
                                     IdEmpresa = p.IdEmpresa,
                                     IdAgenda = p.IdAgenda,
                                     IdAgendaservico = p.IdAgendaservico,
                                     IdProfissional = p.IdProfissional,
                                     IdProfissao = p.IdProfissao,
                                     IdServico = p.IdServico,
                                     IdNavigation = new Profissional
                                     {
                                         Id = new Pessoa
                                         {
                                             DsNomepessoa = p.IdNavigation.Id.DsNomepessoa,
                                         },
                                         IdServicoNavigation = new Servico
                                         {
                                             DsServico = p.IdNavigation.IdServicoNavigation.DsServico
                                         }
                                     }
                                 }).Where(p => /*p.IdEmpresa == NacbarAplicacao.IdEmpresa &&*/ p.IdAgenda == intAgenda)
                                 .OrderBy(p => p.IdNavigation.Id.DsNomepessoa)
                                 .AsNoTracking().ToListAsync();
        }

        public async Task<BuscaCnpj> GetCnpjAsync(string strCnpj)
        {
            using (System.Net.WebClient webCnpj = new System.Net.WebClient())
            {
                webCnpj.Encoding = System.Text.Encoding.UTF8;
                buscaCnpj = Newtonsoft.Json.JsonConvert.DeserializeObject<BuscaCnpj>(webCnpj.DownloadString(@"http://www.receitaws.com.br/v1/cnpj/" + strCnpj));

                buscaCnpj.abertura = buscaCnpj.abertura.Substring(6, 4) + "-" + buscaCnpj.abertura.Substring(3, 2) + "-" + buscaCnpj.abertura.Substring(0, 2);

                buscaCnpj.cep = buscaCnpj.cep.RemoveFormato().FormataTexto(NacbarAplicacao.EnumFormato.CEP);
                buscaCnpj.telefone = buscaCnpj.telefone.RemoveFormato().FormataTexto(NacbarAplicacao.EnumFormato.TELEFONE);

                //ENDERECO
                if (!string.IsNullOrEmpty(buscaCnpj.municipio))
                {
                    buscaCnpj.idEstado = await DBNacbar.Estado.Where(p => p.DsUf.ToUpper() == buscaCnpj.uf.ToUpper())
                                                       .Select(p => p.IdEstado).FirstOrDefaultAsync();

                    buscaCnpj.idMunicipio = await DBNacbar.Municipio.Where(p => p.DsMunicipio.RemoveAcento().ToUpper() == buscaCnpj.municipio.RemoveAcento().ToUpper())
                                                          .Select(p => p.IdMunicipio).FirstOrDefaultAsync();
                }

                if (!string.IsNullOrEmpty(buscaCnpj.bairro))
                {
                    int intCodigoBairro = await DBNacbar.Bairro.Where(p => p.DsBairro.RemoveAcento().ToUpper() == buscaCnpj.bairro.RemoveAcento().ToUpper())
                                                        .Select(p => p.IdBairro).FirstOrDefaultAsync();

                    if (intCodigoBairro == decimal.Zero)
                    {
                        DBNacbar.Bairro.Add(new Bairro()
                        {
                            IdBairro = intCodigoBairro = await DBNacbar.Bairro.MaxAsync(p => (Int32?)p.IdBairro + 1) ?? 1,
                            DsBairro = buscaCnpj.bairro
                        });

                        await DBNacbar.SaveChangesAsync();
                    }
                    buscaCnpj.idBairro = intCodigoBairro;
                }

                buscaCnpj.strObservacao += "TIPO: " + buscaCnpj.tipo + " - ";
                buscaCnpj.strObservacao += "SITUAÇÃO: " + buscaCnpj.situacao + " - ";
                buscaCnpj.strObservacao += "STATUS: " + buscaCnpj.status + "\n";
                buscaCnpj.strObservacao += "ATIVIDADE PRINCIPAL: ";
                foreach (AtividadePrincipal item in buscaCnpj.atividade_principal)
                    buscaCnpj.strObservacao += item.text;
                buscaCnpj.strObservacao += "\nATIVIDADES SECUNDÁRIAS: ";
                foreach (AtividadesSecundarias item in buscaCnpj.atividades_secundarias)
                    buscaCnpj.strObservacao += item.text + "\n";
            }

            return buscaCnpj;
        }

        public async Task<IList<Horario>> GetHorarioAsync(string strDiasemana = null, int? intIdCliente = null, int? intIdProfissional = null,
                                                          int? intIdProfissao = null, int? intIdServico = null)
        {
            return await DBNacbar.Horario
                                       .Select(p => new Horario
                                       {
                                           IdEmpresa = p.IdEmpresa,
                                           IdHorario = p.IdHorario,
                                           IdCliente = p.IdCliente,
                                           Id = new Pessoa { DsNomepessoa = p.Id.DsNomepessoa },
                                           HrHorarioinicial = p.HrHorarioinicial,
                                           HrHorariofinal = p.HrHorariofinal,
                                           IdProfissional = p.IdProfissional,
                                           IdProfissao = p.IdProfissao,
                                           IdServico = p.IdServico,
                                           IdNavigation = new Profissional
                                           {

                                               Id = new Pessoa { DsNomepessoa = p.IdNavigation.Id.DsNomepessoa },
                                               IdProfissaoNavigation = new Tipopessoa { DsTipopessoa = p.IdNavigation.IdProfissaoNavigation.DsTipopessoa },
                                               IdServicoNavigation = new Servico
                                               {
                                                   DsServico = p.IdNavigation.IdServicoNavigation.DsServico,
                                                   NmValor = p.IdNavigation.IdServicoNavigation.NmValor
                                               }
                                           },
                                           CdDiasemana = p.CdDiasemana
                                       })
                                       .Where(p => p.IdEmpresa == NacbarAplicacao.IntIdEmpresa && strDiasemana != null ? p.CdDiasemana == strDiasemana :
                                       (intIdCliente != null && intIdProfissional == null && intIdProfissao == null && intIdServico == null) ?
                                            p.IdCliente == intIdCliente :
                                       (intIdCliente != null && intIdProfissional != null && intIdProfissao != null && intIdServico != null) ?
                                            p.IdCliente == intIdCliente && p.IdProfissional == intIdProfissional &&
                                            p.IdProfissao == intIdProfissao && p.IdServico == intIdServico :
                                       (intIdCliente == null && intIdProfissional != null && intIdProfissao != null && intIdServico != null) ?
                                            p.IdProfissional == intIdProfissional && p.IdProfissao == intIdProfissao
                                            && p.IdServico == intIdServico : false)
                                       .AsNoTracking().ToListAsync();
        }

        public async Task<IList<Municipio>> GetMunicipioAsync(int intEstado)
        {
            return await DBNacbar.Municipio
                                 .Select(p => new Municipio
                                 {
                                     IdMunicipio = p.IdMunicipio,
                                     DsMunicipio = p.DsMunicipio,
                                     IdEstado = p.IdEstado,
                                     Id = new Estado { DsUf = p.Id.DsUf }
                                 }).Where(p => p.IdEstado == intEstado)
                                 .OrderBy(p => p.DsMunicipio)
                                 .AsNoTracking().ToListAsync();
        }

        public async Task<IList<Profissional>> GetProfissionalAsync()
        {
            return await DBNacbar.Profissional
                                 .Select(p => new Profissional
                                 {
                                     IdEmpresa = p.IdEmpresa,
                                     IdProfissional = p.IdProfissional,
                                     IdProfissao = p.IdProfissao,
                                     IdServico = p.IdServico,
                                     IdServicoNavigation = new Servico
                                     {
                                         NmValor = p.IdServicoNavigation.NmValor
                                     },
                                     Id = new Pessoa
                                     {
                                         DsNomepessoa = p.IdServicoNavigation.DsServico + " - " + p.Id.DsNomepessoa + " " + p.IdServicoNavigation.NmValor.Value.ToString("C")
                                     }
                                 }).Where(p => p.IdEmpresa == NacbarAplicacao.IntIdEmpresa && p.IdServicoNavigation.NmValor.HasValue == true)
                                 .AsNoTracking().ToListAsync();
        }

        public async Task<IList<Produto>> GetProdutoAsync()
        {
            return await DBNacbar.Produto
                                 .Select(p => new Produto
                                 {
                                     IdEmpresa = p.IdEmpresa,
                                     IdProduto = p.IdProduto,
                                     DsNomeproduto = p.DsNomeproduto,
                                     DsDesctecnica = p.DsDesctecnica,
                                     NmQtdeabsoluta = 10
                                 }).Where(p => p.IdEmpresa == NacbarAplicacao.IntIdEmpresa)
                                 .AsNoTracking().ToListAsync();
        }

        public async Task<Produto> GetProdutosAsync(int intCodigoProduto)
        {
            return await DBNacbar.Produto
                                 .Select(p => new Produto
                                 {
                                     IdEmpresa = p.IdEmpresa,
                                     IdProduto = p.IdProduto,
                                     DsNomeproduto = p.DsNomeproduto,
                                     DsDesctecnica = p.DsDesctecnica,
                                     DsEan = p.DsEan,
                                     NmValorcompra = p.NmValorcompra,
                                     NmValorvenda = p.NmValorvenda,
                                     NmUnidade = p.NmUnidade,
                                     NmQtdeabsoluta = p.NmQtdeabsoluta,
                                     NmQtderelativa = p.NmQtderelativa,
                                     DsObservacao = p.DsObservacao,
                                     IdCor = p.IdCor,
                                     IdMarca = p.IdMarca,
                                     IdTamanho = p.IdTamanho,
                                     IdTipoproduto = p.IdTipoproduto,
                                     IdUnidade = p.IdUnidade
                                 }).Where(p => p.IdEmpresa == NacbarAplicacao.IntIdEmpresa && p.IdProduto == intCodigoProduto)
                                 .AsNoTracking().FirstOrDefaultAsync();
        }

        public void PostAgendaAsync(Agenda Agenda, List<Agendaservico> LstAgendaServico, List<Agendaproduto> LstAgendaProduto)
        {
            try
            {
                DBNacbar.Agenda.Add(Agenda);

                DBNacbar.Agendaservico.AddRange(LstAgendaServico);

                DBNacbar.Agendaproduto.AddRange(LstAgendaProduto);

                DBNacbar.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PostHorarioAsync(List<Horario> LstHorario)
        {
            try
            {
                DBNacbar.Horario.AddRange(LstHorario);

                DBNacbar.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void PostLoginAsync(int intIdEmpresa, string strNomeEmpresa, bool bolRedirect)
        {
            NacbarAplicacao.IntIdEmpresa = intIdEmpresa;
            NacbarAplicacao.StrNomeEmpresa = strNomeEmpresa;
            NacbarAplicacao.BolRedirect = bolRedirect;
        }
    }
}