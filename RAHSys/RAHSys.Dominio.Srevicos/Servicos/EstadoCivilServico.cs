using System.Collections.Generic;
using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Entidades.Entidades;
using System.Linq;

namespace RAHSys.Dominio.Servicos.Servicos
{
    public class EstadoCivilServico : ServicoBase<EstadoCivilModel>, IEstadoCivilServico
    {
        private readonly IEstadoCivilRepositorio _estadoCivilRepositorio;

        public EstadoCivilServico(IEstadoCivilRepositorio estadoCivilRepositorio) : base(estadoCivilRepositorio)
        {
            _estadoCivilRepositorio = estadoCivilRepositorio;
        }

        public IEnumerable<EstadoCivilModel> ListarTodos()
        {
            return _estadoCivilRepositorio.Consultar().OrderBy(e => e.Descricao).ToList();
        }
    }
}
