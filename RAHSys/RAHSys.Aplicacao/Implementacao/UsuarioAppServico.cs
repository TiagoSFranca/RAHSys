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
    public class UsuarioAppServico : AppServicoBase<UsuarioModel>, IUsuarioAppServico
    {
        private readonly IUsuarioServico _usuarioServico;

        public UsuarioAppServico(IUsuarioServico usuarioServico) : base(usuarioServico)
        {
            _usuarioServico = usuarioServico;
        }

        public void Adicionar(UsuarioAppModel obj)
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

        public UsuarioAppModel ObterPorId(int id)
        {
            try
            {
                return _usuarioServico.ObterPorId(id).MapearParaAplicacao();
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

        public void Atualizar(UsuarioAppModel obj)
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

        public List<UsuarioAppModel> ListarTodos(IEnumerable<string> idNaoListar)
        {
            try
            {
                return _usuarioServico.ListarTodos(idNaoListar).Select(e => e.MapearParaAplicacao()).ToList();
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
