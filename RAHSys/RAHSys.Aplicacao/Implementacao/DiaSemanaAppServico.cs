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
    public class DiaSemanaAppServico : AppServicoBase<DiaSemanaModel>, IDiaSemanaAppServico
    {
        private readonly IDiaSemanaServico _diaSemanaServico;

        public DiaSemanaAppServico(IDiaSemanaServico diaSemanaServico) : base(diaSemanaServico)
        {
            _diaSemanaServico = diaSemanaServico;
        }

        public void Adicionar(DiaSemanaAppModel obj)
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

        public DiaSemanaAppModel ObterPorId(int id)
        {
            try
            {
                return _diaSemanaServico.ObterPorId(id).MapearParaAplicacao();
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

        public void Atualizar(DiaSemanaAppModel obj)
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

        public List<DiaSemanaAppModel> ListarTodos()
        {
            try
            {
                return _diaSemanaServico.ListarTodos().Select(e => e.MapearParaAplicacao()).ToList();
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
