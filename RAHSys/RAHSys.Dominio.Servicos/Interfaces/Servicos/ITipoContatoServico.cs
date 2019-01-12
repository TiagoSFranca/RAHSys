using RAHSys.Entidades;
using RAHSys.Entidades.Entidades;
using System.Collections.Generic;

namespace RAHSys.Dominio.Servicos.Interfaces.Servicos
{
    public interface ITipoContatoServico : IServicoBase<TipoContatoModel>
    {
        ConsultaModel<TipoContatoModel> Consultar(IEnumerable<int> idList, string descricao, string ordenacao, bool crescente, int pagina, int quantidade);
    }
}
