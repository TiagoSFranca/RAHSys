using System.Collections.Generic;
using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Entidades.Entidades;
using System.Linq;

namespace RAHSys.Dominio.Servicos.Servicos
{
    public class EstadoServico : ServicoBase<EstadoModel>, IEstadoServico
    {
        private readonly IEstadoRepositorio _estadoRepositorio;

        public EstadoServico(IEstadoRepositorio estadoRepositorio) : base(estadoRepositorio)
        {
            _estadoRepositorio = estadoRepositorio;
        }

        public IEnumerable<EstadoModel> ListarTodos()
        {
            return _estadoRepositorio.Consultar().OrderBy(e => e.Descricao).ToList();
        }
    }
}
