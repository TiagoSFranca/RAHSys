using System.Collections.Generic;
using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Entidades.Entidades;
using System.Linq;

namespace RAHSys.Dominio.Servicos.Servicos
{
    public class TipoRecorrenciaServico : ServicoBase<TipoRecorrenciaModel>, ITipoRecorrenciaServico
    {
        private readonly ITipoRecorrenciaRepositorio _tipoRecorrenciaRepositorio;

        public TipoRecorrenciaServico(ITipoRecorrenciaRepositorio estadoRepositorio) : base(estadoRepositorio)
        {
            _tipoRecorrenciaRepositorio = estadoRepositorio;
        }

        public IEnumerable<TipoRecorrenciaModel> ListarTodos()
        {
            return _tipoRecorrenciaRepositorio.Consultar().OrderBy(e => e.IdTipoRecorrencia).ToList();
        }
    }
}
