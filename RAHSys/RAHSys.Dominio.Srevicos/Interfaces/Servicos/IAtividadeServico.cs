using RAHSys.Entidades;
using RAHSys.Entidades.Entidades;
using System;
using System.Collections.Generic;

namespace RAHSys.Dominio.Servicos.Interfaces.Servicos
{
    public interface IAtividadeServico : IServicoBase<AtividadeModel>
    {
        ConsultaModel<AtividadeRecorrenciaModel> Consultar(IEnumerable<int> idList, IEnumerable<int> idTipoAtividadeList, IEnumerable<int> idEquipeList,
            IEnumerable<int> idContratoList, IEnumerable<string> idUsuarioList, string mesAno, bool? realizada,
            string ordenacao, bool crescente, int pagina, int quantidade);
        void FinalizarAtividade(int idAtividade, DateTime dataRealizacaoPrevista, DateTime dataRealizacao, string observacao);
        void TransferirAtividade(int idAtividade, string idUsuario);
    }
}
