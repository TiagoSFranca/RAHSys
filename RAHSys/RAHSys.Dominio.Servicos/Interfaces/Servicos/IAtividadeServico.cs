using RAHSys.Entidades;
using RAHSys.Entidades.Entidades;
using System;
using System.Collections.Generic;

namespace RAHSys.Dominio.Servicos.Interfaces.Servicos
{
    public interface IAtividadeServico : IServicoBase<AtividadeModel>
    {
        ConsultaModel<AtividadeRecorrenciaModel> Consultar(IEnumerable<int> idList, IEnumerable<int> idTipoAtividadeList, IEnumerable<int> idEquipeList,
            IEnumerable<int> idContratoList, IEnumerable<string> idUsuarioList, DateTime dataInicial, DateTime dataFinal,
            string ordenacao, bool crescente, int pagina, int quantidade);
        void TransferirAtividade(int idAtividade, string idUsuario);
        void CopiarAtividade(int idAtividade);
        void EncerrarAtividade(int idAtividade, DateTime dataEncerramento);
        void AlterarEquipe(int idAtividade, int idEquipe);
    }
}
