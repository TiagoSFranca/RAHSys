using RAHSys.Aplicacao.AppModels;
using RAHSys.Aplicacao.Interfaces;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Entidades.Entidades;
using RAHSys.Infra.CrossCutting.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using RAHSys.Aplicacao.Extensoes;

namespace RAHSys.Aplicacao.Implementacao
{
    public class TipoTelhadoAppServico : AppServicoBase<TipoTelhadoModel>, ITipoTelhadoAppServico
    {
        private readonly ITipoTelhadoServico _tipoTelhadoServico;

        public TipoTelhadoAppServico(ITipoTelhadoServico tipoTelhadoServico) : base(tipoTelhadoServico)
        {
            _tipoTelhadoServico = tipoTelhadoServico;
        }

        public void Adicionar(TipoTelhadoAppModel obj)
        {
            try
            {
                _tipoTelhadoServico.Adicionar(obj.MapearParaDominio());
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

        public ConsultaAppModel<TipoTelhadoAppModel> Consultar(IEnumerable<int> idList, string descricao, string ordenacao, bool crescente, int pagina, int quantidade)
        {
            try
            {
                var consulta = new ConsultaAppModel<TipoTelhadoAppModel>();

                var resultado = _tipoTelhadoServico.Consultar(idList, descricao, ordenacao, crescente, pagina, quantidade);

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

        public TipoTelhadoAppModel ObterPorId(int id)
        {
            try
            {
                return _tipoTelhadoServico.ObterPorId(id).MapearParaAplicacao();
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
                var tipoTelhado = _tipoTelhadoServico.ObterPorId(id);
                _tipoTelhadoServico.Remover(tipoTelhado);
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

        public void Atualizar(TipoTelhadoAppModel obj)
        {
            try
            {
                _tipoTelhadoServico.Atualizar(obj.MapearParaDominio());
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
