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
    public class AuditoriaAppServico : AppServicoAuditoriaBase<AuditoriaModel>, IAuditoriaAppServico
    {
        private readonly IAuditoriaServico _auditoriaServico;

        public AuditoriaAppServico(IAuditoriaServico auditoriaServico) : base(auditoriaServico)
        {
            _auditoriaServico = auditoriaServico;
        }

        public void Adicionar(AuditoriaAppModel obj)
        {
            try
            {
                _auditoriaServico.Adicionar(obj.MapearParaDominio());
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

        public void Atualizar(AuditoriaAppModel obj)
        {
            // Do nothing
        }

        public ConsultaAppModel<AuditoriaAppModel> Consultar(IEnumerable<int> idList, string usuario, string funcao, string acao, string enderecoIp, DateTime dataHora, string ordenacao, bool crescente, int pagina, int quantidade)
        {
            try
            {
                var consulta = new ConsultaAppModel<AuditoriaAppModel>();


                var resultado = _auditoriaServico.Consultar(idList, usuario, funcao, acao, enderecoIp, dataHora, ordenacao, crescente, pagina, quantidade);

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

        public AuditoriaAppModel ObterPorId(int id)
        {
            try
            {
                return _auditoriaServico.ObterPorId(id).MapearParaAplicacao();
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
            // Do nothing
        }
    }
}
