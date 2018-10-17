using RAHSys.Aplicacao.AppModels;
using System;
using System.Collections.Generic;

namespace RAHSys.Aplicacao.Interfaces
{
    public interface IAtividadeAppServico : IAppServicoBase<AtividadeAppModel>
    {
        ConsultaAppModel<AtividadeAppModel> Consultar(IEnumerable<int> idList, IEnumerable<int> idTipoAtividadeList, IEnumerable<int> idEquipeList,
            IEnumerable<int> idContratoList, IEnumerable<string> idUsuarioList, bool? realizada, string dataRealizacaoInicio, string dataRealizacaoFim,
            string dataPrevistaInicio, string dataPrevistaFim,
            string ordenacao, bool crescente, int pagina, int quantidade);
    }
}
