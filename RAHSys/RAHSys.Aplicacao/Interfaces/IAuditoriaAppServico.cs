using RAHSys.Aplicacao.AppModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAHSys.Aplicacao.Interfaces
{
    public interface IAuditoriaAppServico : IAppServicoBase<AuditoriaAppModel>
    {
        ConsultaAppModel<AuditoriaAppModel> Consultar(IEnumerable<int> idList, string usuario, string funcao, string acao, string enderecoIp, DateTime data, string ordenacao, bool crescente, int pagina, int quantidade);
    }
}
