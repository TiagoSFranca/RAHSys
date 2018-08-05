using RAHSys.Entidades;
using RAHSys.Entidades.Entidades;
using System.Collections;
using System.Collections.Generic;

namespace RAHSys.Dominio.Servicos.Interfaces.Servicos
{
    public interface ICameraServico : IServicoBase<CameraModel>
    {
        ConsultaModel<CameraModel> Consultar(IEnumerable<int> idList, string localizacao, string descricao, int pagina, int quantidade);
    }
}
