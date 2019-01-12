using RAHSys.Entidades;
using RAHSys.Entidades.Entidades;
using System.Collections.Generic;

namespace RAHSys.Dominio.Servicos.Interfaces.Servicos
{
    public interface IEquipeServico : IServicoBase<EquipeModel>
    {
        ConsultaModel<EquipeModel> Consultar(IEnumerable<int> idList, string email,
            string ordenacao, bool crescente, int pagina, int quantidade);

        IEnumerable<EquipeModel> ListarTodos();
    }
}
