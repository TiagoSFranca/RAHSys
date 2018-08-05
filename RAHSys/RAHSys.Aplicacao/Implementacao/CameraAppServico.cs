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
    public class CameraAppServico : AppServicoBase<CameraModel>, ICameraAppServico
    {
        private readonly ICameraServico _cameraServico;

        public CameraAppServico(ICameraServico cameraServico) : base(cameraServico)
        {
            _cameraServico = cameraServico;
        }

        public void Adicionar(CameraAppModel obj)
        {
            try
            {
                _cameraServico.Adicionar(obj.MapearParaDominio());
            }
            catch (Exception ex)
            {
                var nex = new CustomBaseException(ex);
                LogExceptions(nex);
                throw nex;
            }
        }

        public ConsultaAppModel<CameraAppModel> Consultar(IEnumerable<int> idList, string localizacao, string descricao, string ordenacao, bool crescente, int pagina, int quantidade)
        {
            try
            {
                var consulta = new ConsultaAppModel<CameraAppModel>();

                var resultado = _cameraServico.Consultar(idList, localizacao, descricao, ordenacao, crescente, pagina, quantidade);

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

        public CameraAppModel ObterPorId(int id)
        {
            try
            {
                return _cameraServico.ObterPorId(id).MapearParaAplicacao();
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
                var camera = _cameraServico.ObterPorId(id);
                _cameraServico.Remover(camera);
            }
            catch (Exception ex)
            {
                var nex = new CustomBaseException(ex);
                LogExceptions(nex);
                throw nex;
            }
        }

        public void Atualizar(CameraAppModel obj)
        {
            try
            {
                _cameraServico.Atualizar(obj.MapearParaDominio());
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
