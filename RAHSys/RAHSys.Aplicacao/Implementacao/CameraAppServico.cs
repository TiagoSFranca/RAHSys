using RAHSys.Aplicacao.Interfaces;
using RAHSys.Entidades.Entidades;
using System;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Aplicacao.AppModels;
using RAHSys.Aplicacao.Extensoes;
using System.Collections.Generic;
using System.Linq;

namespace RAHSys.Aplicacao.Implementacao
{
    public class CameraAppServico : AppServicoBase<CameraModel>, ICameraAppServico
    {
        private readonly ICameraServico _cameraServico;

        public CameraAppServico(ICameraServico cameraServico) : base(cameraServico)
        {
            _cameraServico = cameraServico;
        }

        public void Add(CameraAppModel obj)
        {
            try
            {
                _cameraServico.Add(obj.MapearParaDominio());
            }
            catch (Exception ex)
            {
                LogExceptions(ex);
                throw;
            }
        }

        public ConsultaAppModel<CameraAppModel> Consultar(IEnumerable<int> idList, string localizacao, string descricao, string ordenacao, bool crescente, int pagina, int quantidade)
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

        public CameraAppModel GetById(int id)
        {
            try
            {
                return _cameraServico.GetById(id).MapearParaAplicacao();
            }
            catch (Exception ex)
            {
                LogExceptions(ex);
                throw;
            }
        }

        public void Remove(int id)
        {
            try
            {
                var camera = _cameraServico.GetById(id);
                _cameraServico.Remove(camera);
            }
            catch (Exception ex)
            {
                LogExceptions(ex);
                throw;
            }
        }

        public void Update(CameraAppModel obj)
        {
            try
            {
                _cameraServico.Update(obj.MapearParaDominio());
            }
            catch (Exception ex)
            {
                LogExceptions(ex);
                throw;
            }
        }
    }
}
