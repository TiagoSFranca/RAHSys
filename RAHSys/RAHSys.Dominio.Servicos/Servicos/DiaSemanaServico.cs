using System.Collections.Generic;
using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Entidades.Entidades;
using System.Linq;

namespace RAHSys.Dominio.Servicos.Servicos
{
    public class DiaSemanaServico : ServicoBase<DiaSemanaModel>, IDiaSemanaServico
    {
        private readonly IDiaSemanaRepositorio _diaSemanaRepositorio;

        public DiaSemanaServico(IDiaSemanaRepositorio diaSemanaRepositorio) : base(diaSemanaRepositorio)
        {
            _diaSemanaRepositorio = diaSemanaRepositorio;
        }

        public IEnumerable<DiaSemanaModel> ListarTodos()
        {
            return _diaSemanaRepositorio.Consultar().ToList();
        }
    }
}
