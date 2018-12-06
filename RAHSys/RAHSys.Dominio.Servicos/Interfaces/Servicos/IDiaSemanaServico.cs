using RAHSys.Entidades.Entidades;
using System.Collections.Generic;

namespace RAHSys.Dominio.Servicos.Interfaces.Servicos
{
    public interface IDiaSemanaServico : IServicoBase<DiaSemanaModel>
    {
        IEnumerable<DiaSemanaModel> ListarTodos();
    }
}
