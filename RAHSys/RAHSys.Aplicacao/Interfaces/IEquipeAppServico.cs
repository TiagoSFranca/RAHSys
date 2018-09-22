using RAHSys.Aplicacao.AppModels;
using System.Collections.Generic;

namespace RAHSys.Aplicacao.Interfaces
{
    public interface IEquipeAppServico : IAppServicoBase<EquipeAppModel>
    {
        ConsultaAppModel<EquipeAppModel> Consultar(IEnumerable<int> idList, string email,
            string ordenacao, bool crescente, int pagina, int quantidade);
    }
}