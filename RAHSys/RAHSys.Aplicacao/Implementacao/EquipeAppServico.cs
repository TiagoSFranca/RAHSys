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
    public class EquipeAppServico : AppServicoBase<EquipeModel>, IEquipeAppServico
    {
        private readonly IEquipeServico _equipeServico;

        public EquipeAppServico(IEquipeServico equipeServico) : base(equipeServico)
        {
            _equipeServico = equipeServico;
        }

        public void Adicionar(EquipeAppModel obj)
        {
            try
            {
                _equipeServico.Adicionar(obj.MapearParaDominio());
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

        public EquipeAppModel ObterPorId(int id)
        {
            try
            {
                return _equipeServico.ObterPorId(id).MapearParaAplicacao();
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
                var equipe = _equipeServico.ObterPorId(id);
                _equipeServico.Remover(equipe);
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

        public void Atualizar(EquipeAppModel obj)
        {
            try
            {
                _equipeServico.Atualizar(obj.MapearParaDominio());
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

        public ConsultaAppModel<EquipeAppModel> Consultar(IEnumerable<int> idList, string email, string ordenacao, bool crescente, int pagina, int quantidade)
        {
            try
            {
                var consulta = new ConsultaAppModel<EquipeAppModel>();

                var resultado = _equipeServico.Consultar(idList, email, ordenacao, crescente, pagina, quantidade);

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

        public List<EquipeAppModel> ListarTodos()
        {
            try
            {
                return _equipeServico.ListarTodos().Select(e => e.MapearParaAplicacao()).ToList();
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
