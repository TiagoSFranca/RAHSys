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
    public class ContratoAppServico : AppServicoBase<ContratoModel>, IContratoAppServico
    {
        private readonly IContratoServico _contratoServico;

        public ContratoAppServico(IContratoServico contratoServico) : base(contratoServico)
        {
            _contratoServico = contratoServico;
        }

        public void Adicionar(ContratoAppModel obj)
        {
            try
            {
                _contratoServico.Adicionar(obj.MapearParaDominio());
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

        public ConsultaAppModel<ContratoAppModel> Consultar(IEnumerable<int> idList, string nomeEmpresa, string cidade, string ordenacao, bool crescente, int pagina, int quantidade)
        {
            try
            {
                var consulta = new ConsultaAppModel<ContratoAppModel>();

                var resultado = _contratoServico.Consultar(idList, nomeEmpresa, cidade, ordenacao, crescente, pagina, quantidade);

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

        public ContratoAppModel ObterPorId(int id)
        {
            try
            {
                return _contratoServico.ObterPorId(id).MapearParaAplicacao();
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
                var contrato = _contratoServico.ObterPorId(id);
                _contratoServico.Remover(contrato);
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

        public void Atualizar(ContratoAppModel obj)
        {
            try
            {
                _contratoServico.Atualizar(obj.MapearParaDominio());
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

        public void AdicionarAnaliseInvestimento(AnaliseInvestimentoAppModel analiseInvestimento)
        {
            try
            {
                _contratoServico.AdicionarAnaliseInvestimento(analiseInvestimento.MapearParaDominio());
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
