using RAHSys.Entidades.Entidades;
using System.Collections.Generic;

namespace RAHSys.Dominio.Servicos.Interfaces.Servicos
{
    public interface ICidadeServico : IServicoBase<CidadeModel>
    {
        IEnumerable<CidadeModel> ObterCidadesPorEstado(int idEstado);
    }
}
