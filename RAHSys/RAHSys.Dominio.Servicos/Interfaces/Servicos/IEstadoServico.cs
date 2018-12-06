using RAHSys.Entidades.Entidades;
using System.Collections.Generic;

namespace RAHSys.Dominio.Servicos.Interfaces.Servicos
{
    public interface IEstadoServico : IServicoBase<EstadoModel>
    {
        IEnumerable<EstadoModel> ListarTodos();
    }
}
