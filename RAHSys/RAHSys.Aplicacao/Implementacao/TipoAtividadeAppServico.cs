using RAHSys.Aplicacao.Interfaces;
using RAHSys.Entidades.Entidades;
using System;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Aplicacao.Extensoes;
using System.Collections.Generic;
using System.Linq;
using RAHSys.Infra.CrossCutting.Exceptions;

namespace RAHSys.Aplicacao.Implementacao
{
    public class TipoAtividadeAppServico : AppServicoBase<TipoAtividadeModel>, ITipoAtividadeAppServico
    {
        private readonly ITipoAtividadeServico _tipoAtividadeServico;

        public TipoAtividadeAppServico(ITipoAtividadeServico tipoAtividadeServico) : base(tipoAtividadeServico)
        {
            _tipoAtividadeServico = tipoAtividadeServico;
        }

        public void Adicionar(TipoAtividadeAppModel obj)
        {
            try
            {
                _tipoAtividadeServico.Adicionar(obj.MapearParaDominio());
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

        public ConsultaAppModel<TipoAtividadeAppModel> Consultar(IEnumerable<int> idList, string descricao, string ordenacao, bool crescente, int pagina, int quantidade)
        {
            try
            {
                var consulta = new ConsultaAppModel<TipoAtividadeAppModel>();

                var resultado = _tipoAtividadeServico.Consultar(idList, descricao, ordenacao, crescente, pagina, quantidade);

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

        public TipoAtividadeAppModel ObterPorId(int id)
        {
            try
            {
                return _tipoAtividadeServico.ObterPorId(id).MapearParaAplicacao();
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
                var tipoAtividade = _tipoAtividadeServico.ObterPorId(id);
                _tipoAtividadeServico.Remover(tipoAtividade);
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

        public void Atualizar(TipoAtividadeAppModel obj)
        {
            try
            {
                _tipoAtividadeServico.Atualizar(obj.MapearParaDominio());
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

        public List<TipoAtividadeAppModel> ListarTodos()
        {
            try
            {
                return _tipoAtividadeServico.ListarTodos().Select(e => e.MapearParaAplicacao()).ToList();
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
