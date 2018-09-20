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
    public class PagamentoAppServico : AppServicoBase<PagamentoModel>, IPagamentoAppServico
    {
        private readonly IPagamentoServico _pagamentoServico;

        public PagamentoAppServico(IPagamentoServico pagamentoServico) : base(pagamentoServico)
        {
            _pagamentoServico = pagamentoServico;
        }

        public void Adicionar(PagamentoAppModel obj)
        {
            try
            {
                _pagamentoServico.Adicionar(obj.MapearParaDominio());
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

        public PagamentoAppModel ObterPorId(int id)
        {
            try
            {
                return _pagamentoServico.ObterPorId(id).MapearParaAplicacao();
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
                var pagamento = _pagamentoServico.ObterPorId(id);
                _pagamentoServico.Remover(pagamento);
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

        public void Atualizar(PagamentoAppModel obj)
        {
            try
            {
                _pagamentoServico.Atualizar(obj.MapearParaDominio());
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

        public ConsultaAppModel<PagamentoAppModel> Consultar(int idContrato, IEnumerable<int> idList, string ordenacao, bool crescente, int pagina, int quantidade)
        {
            try
            {
                var consulta = new ConsultaAppModel<PagamentoAppModel>();

                var resultado = _pagamentoServico.Consultar(idContrato, idList, ordenacao, crescente, pagina, quantidade);

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
    }
}
