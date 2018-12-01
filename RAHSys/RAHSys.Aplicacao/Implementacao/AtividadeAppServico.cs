using RAHSys.Aplicacao.Interfaces;
using RAHSys.Entidades.Entidades;
using System;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Aplicacao.Extensoes;
using RAHSys.Infra.CrossCutting.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace RAHSys.Aplicacao.Implementacao
{
    public class AtividadeAppServico : AppServicoBase<AtividadeModel>, IAtividadeAppServico
    {
        private readonly IAtividadeServico _atividadeServico;

        public AtividadeAppServico(IAtividadeServico atividadeServico) : base(atividadeServico)
        {
            _atividadeServico = atividadeServico;
        }

        public void Adicionar(AtividadeAppModel obj)
        {
            try
            {
                var atividade = obj.MapearParaDominio();

                if (string.IsNullOrEmpty(atividade.IdUsuario))
                    atividade.IdUsuario = null;

                _atividadeServico.Adicionar(atividade);
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

        public AtividadeAppModel ObterPorId(int id)
        {
            try
            {
                return _atividadeServico.ObterPorId(id).MapearParaAplicacao();
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
                var atividade = _atividadeServico.ObterPorId(id);
                _atividadeServico.Remover(atividade);
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

        public void Atualizar(AtividadeAppModel obj)
        {
            try
            {
                var atividade = obj.MapearParaDominio();

                if (string.IsNullOrEmpty(atividade.IdUsuario))
                    atividade.IdUsuario = null;

                _atividadeServico.Atualizar(atividade);
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

        public ConsultaAppModel<AtividadeRecorrenciaAppModel> Consultar(IEnumerable<int> idList, IEnumerable<int> idTipoAtividadeList, IEnumerable<int> idEquipeList,
            IEnumerable<int> idContratoList, IEnumerable<string> idUsuarioList, string mesAno, bool? realizada,
            string ordenacao, bool crescente, int pagina, int quantidade)
        {
            try
            {
                var consulta = new ConsultaAppModel<AtividadeRecorrenciaAppModel>();

                var resultado = _atividadeServico.Consultar(idList, idTipoAtividadeList, idEquipeList,
                    idContratoList, idUsuarioList, mesAno, realizada,
                    ordenacao, crescente, pagina, quantidade);

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

        public void FinalizarRecorrencia(int idAtividade, DateTime dataRealizacaoPrevista, DateTime dataRealizacao, string observacao)
        {
            try
            {
                _atividadeServico.FinalizarRecorrencia(idAtividade, dataRealizacaoPrevista, dataRealizacao, observacao);
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

        public void TransferirAtividade(int idAtividade, string idUsuario)
        {
            try
            {
                _atividadeServico.TransferirAtividade(idAtividade, idUsuario);
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

        public void CopiarAtividade(int idAtividade)
        {
            try
            {
                _atividadeServico.CopiarAtividade(idAtividade);
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

        public void EncerrarAtividade(int idAtividade)
        {
            try
            {
                _atividadeServico.EncerrarAtividade(idAtividade);
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
