using RAHSys.Aplicacao.AppModels;
using RAHSys.Aplicacao.Extensoes;
using RAHSys.Aplicacao.Interfaces;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Entidades.Entidades;
using RAHSys.Infra.CrossCutting.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RAHSys.Aplicacao.Implementacao
{
    public class RegistroRecorrenciaAppServico : AppServicoBase<RegistroRecorrenciaModel>, IRegistroRecorrenciaAppServico
    {
        private readonly IRegistroRecorrenciaServico _registroRecorrenciaServico;

        public RegistroRecorrenciaAppServico(IRegistroRecorrenciaServico registroRecorrenciaServico) : base(registroRecorrenciaServico)
        {
            _registroRecorrenciaServico = registroRecorrenciaServico;
        }

        public void Adicionar(RegistroRecorrenciaAppModel obj)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (CustomBaseException ex)
            {
                LogExceptions(ex);
                throw;
            }
            catch (Exception ex)
            {
                var nex = new CustomBaseException(ex);
                LogExceptions(nex);
                throw nex;
            }
        }

        public RegistroRecorrenciaAppModel ObterPorId(int id)
        {
            try
            {
                return _registroRecorrenciaServico.ObterPorId(id).MapearParaAplicacao();
            }
            catch (Exception ex)
            {
                var nex = new CustomBaseException(ex);
                LogExceptions(nex);
                throw nex;
            }
        }

        public void Remover(int id)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (CustomBaseException ex)
            {
                LogExceptions(ex);
                throw;
            }
            catch (Exception ex)
            {
                var nex = new CustomBaseException(ex);
                LogExceptions(nex);
                throw nex;
            }
        }

        public void Atualizar(RegistroRecorrenciaAppModel obj)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (CustomBaseException ex)
            {
                LogExceptions(ex);
                throw;
            }
            catch (Exception ex)
            {
                var nex = new CustomBaseException(ex);
                LogExceptions(nex);
                throw nex;
            }
        }

        public ConsultaAppModel<RegistroRecorrenciaAppModel> Consultar(int idAtividade, IEnumerable<int> idList, DateTime? dataPrevista, DateTime? dataRealizacao, string ordenacao, bool crescente, int pagina, int quantidade)
        {
            try
            {
                var consulta = new ConsultaAppModel<RegistroRecorrenciaAppModel>();

                var resultado = _registroRecorrenciaServico.Consultar(idAtividade, idList, dataPrevista, dataRealizacao, ordenacao, crescente, pagina, quantidade);

                consulta.ItensPorPagina = resultado.ItensPorPagina;
                consulta.PaginaAtual = resultado.PaginaAtual;
                consulta.TotalPaginas = resultado.TotalPaginas;
                consulta.TotalItens = resultado.TotalItens;

                consulta.Resultado = resultado.Resultado.Select(r => r.MapearParaAplicacao()).ToList();

                return consulta;
            }
            catch (Exception ex)
            {
                var nex = new CustomBaseException(ex);
                LogExceptions(nex);
                throw nex;
            }
        }

        public void FinalizarRegistroRecorrencia(int idAtividade, DateTime dataRealizacaoPrevista, List<ArquivoAppModel> evidencias)
        {
            try
            {
                _registroRecorrenciaServico.FinalizarRegistroRecorrencia(idAtividade, dataRealizacaoPrevista, evidencias.Select(e => e.MapearParaDominio()).ToList());
            }
            catch (CustomBaseException ex)
            {
                LogExceptions(ex);
                throw;
            }
            catch (Exception ex)
            {
                var nex = new CustomBaseException(ex);
                LogExceptions(nex);
                throw nex;
            }
        }
    }
}
