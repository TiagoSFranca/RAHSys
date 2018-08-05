using System.Collections.Generic;
using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Entidades.Entidades;
using System.Linq;
using RAHSys.Entidades;

namespace RAHSys.Dominio.Servicos.Servicos
{
    public class CameraServico : ServicoBase<CameraModel>, ICameraServico
    {
        private readonly ICameraRepositorio _cameraRepositorio;

        public CameraServico(ICameraRepositorio cameraRepositorio) : base(cameraRepositorio)
        {
            _cameraRepositorio = cameraRepositorio;
        }

        public ConsultaModel<CameraModel> Consultar(IEnumerable<int> idList, string localizacao, string descricao, string ordenacao, bool crescente, int pagina, int quantidade)
        {
            var consultaModel = new ConsultaModel<CameraModel>(pagina, quantidade);

            var query = _cameraRepositorio.Consultar();
            if (idList?.Count() > 0)
                query = query.Where(c => idList.Contains(c.IdCamera));

            if (!string.IsNullOrEmpty(localizacao))
                query = query.Where(c => c.Localizacao.ToLower().Contains(localizacao.ToLower()));

            if (!string.IsNullOrEmpty(descricao))
                query = query.Where(c => c.Descricao.ToLower().Contains(descricao.ToLower()));

            switch ((ordenacao ?? string.Empty).ToLower())
            {
                case "localização":
                    query = crescente ? query.OrderBy(c => c.Localizacao) : query.OrderByDescending(c => c.Localizacao);
                    break;
                case "descrição":
                    query = crescente ? query.OrderBy(c => c.Descricao) : query.OrderByDescending(c => c.Descricao);
                    break;
                default:
                    query = crescente ? query.OrderBy(c => c.IdCamera) : query.OrderByDescending(c => c.IdCamera);
                    break;

            }
            var resultado = query.Skip((pagina == 1 ? 0 : pagina - 1) * quantidade).Take(quantidade).ToList();
            consultaModel.TotalItens = query.Count();
            consultaModel.Resultado = resultado;

            return consultaModel;
        }
    }
}
