using RAHSys.Entidades;
using RAHSys.Entidades.Entidades;
using System;
using System.Collections.Generic;

namespace RAHSys.Dominio.Servicos.Interfaces.Servicos
{
    public interface IAtividadeServico : IServicoBase<AtividadeModel>
    {
        ConsultaModel<AtividadeModel> Consultar(IEnumerable<int> idList, IEnumerable<int> idTipoAtividadeList, IEnumerable<int> idEquipeList,
            IEnumerable<int> idContratoList, IEnumerable<string> idUsuarioList, bool? realizada, string dataRealizacaoInicio, string dataRealizacaoFim,
            string dataPrevistaInicio, string dataPrevistaFim,
            string ordenacao, bool crescente, int pagina, int quantidade);
    }
}
