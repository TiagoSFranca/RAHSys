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
    public class TipoContatoAppServico : AppServicoBase<TipoContatoModel>, ITipoContatoAppServico
    {
        private readonly ITipoContatoServico _tipoContatoServico;

        public TipoContatoAppServico(ITipoContatoServico tipoContatoServico) : base(tipoContatoServico)
        {
            _tipoContatoServico = tipoContatoServico;
        }

        public void Adicionar(TipoContatoAppModel obj)
        {
            try
            {
                _tipoContatoServico.Adicionar(obj.MapearParaDominio());
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

        public ConsultaAppModel<TipoContatoAppModel> Consultar(IEnumerable<int> idList, string descricao, string ordenacao, bool crescente, int pagina, int quantidade)
        {
            try
            {
                var consulta = new ConsultaAppModel<TipoContatoAppModel>();

                var resultado = _tipoContatoServico.Consultar(idList, descricao, ordenacao, crescente, pagina, quantidade);

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

        public TipoContatoAppModel ObterPorId(int id)
        {
            try
            {
                return _tipoContatoServico.ObterPorId(id).MapearParaAplicacao();
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
                var tipoContato = _tipoContatoServico.ObterPorId(id);
                _tipoContatoServico.Remover(tipoContato);
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

        public void Atualizar(TipoContatoAppModel obj)
        {
            try
            {
                _tipoContatoServico.Atualizar(obj.MapearParaDominio());
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
