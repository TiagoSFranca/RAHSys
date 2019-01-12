using System.Collections.Generic;
using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Entidades.Entidades;
using System.Linq;

namespace RAHSys.Dominio.Servicos.Servicos
{
    public class CidadeServico : ServicoBase<CidadeModel>, ICidadeServico
    {
        private readonly ICidadeRepositorio _cidadeRepositorio;

        public CidadeServico(ICidadeRepositorio cidadeRepositorio) : base(cidadeRepositorio)
        {
            _cidadeRepositorio = cidadeRepositorio;
        }

        public IEnumerable<CidadeModel> ObterCidadesPorEstado(int idEstado)
        {
            var query = _cidadeRepositorio.Consultar();
            return query.Where(c => c.IdEstado == idEstado).ToList();
        }
    }
}
