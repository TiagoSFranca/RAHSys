using RAHSys.Aplicacao.AppModels;
using System;
using System.Collections.Generic;

namespace RAHSys.Aplicacao.Interfaces
{
    public interface IAtividadeAppServico : IAppServicoBase<AtividadeAppModel>
    {
        ConsultaAppModel<AtividadeRecorrenciaAppModel> Consultar(IEnumerable<int> idList, IEnumerable<int> idTipoAtividadeList, IEnumerable<int> idEquipeList,
            IEnumerable<int> idContratoList, IEnumerable<string> idUsuarioList, DateTime dataInicial, DateTime dataFinal,
            string ordenacao, bool crescente, int pagina, int quantidade);
        void TransferirAtividade(int idAtividade, string idUsuario);
        void CopiarAtividade(int idAtividade);
        void EncerrarAtividade(int idAtividade, DateTime dataEncerramento);
        void AlterarEquipe(int idAtividade, int idEquipe);
    }
}
