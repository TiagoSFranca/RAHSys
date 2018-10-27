using RAHSys.Entidades.Entidades;
using System.Collections.Generic;

namespace RAHSys.Dominio.Servicos.Interfaces.Servicos
{
    public interface ITipoRecorrenciaServico : IServicoBase<TipoRecorrenciaModel>
    {
        IEnumerable<TipoRecorrenciaModel> ListarTodos();
    }
}
