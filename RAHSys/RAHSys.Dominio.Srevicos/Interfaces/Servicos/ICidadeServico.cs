using RAHSys.Entidades.Entidades;
using System.Collections.Generic;

namespace RAHSys.Dominio.Servicos.Interfaces.Servicos
{
    public interface ICidadeServico : IServicoBase<CidadeModel>
    {
        List<CidadeModel> ObterCidadesPorEstado(int idEstado);
    }
}
