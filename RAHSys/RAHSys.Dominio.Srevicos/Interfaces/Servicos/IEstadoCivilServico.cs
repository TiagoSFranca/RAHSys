using RAHSys.Entidades.Entidades;
using System.Collections.Generic;

namespace RAHSys.Dominio.Servicos.Interfaces.Servicos
{
    public interface IEstadoCivilServico : IServicoBase<EstadoCivilModel>
    {
        IEnumerable<EstadoCivilModel> ListarTodos();
    }
}
