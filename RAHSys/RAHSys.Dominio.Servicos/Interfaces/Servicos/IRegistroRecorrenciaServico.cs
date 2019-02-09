using RAHSys.Entidades;
using RAHSys.Entidades.Entidades;
using System;
using System.Collections.Generic;

namespace RAHSys.Dominio.Servicos.Interfaces.Servicos
{
    public interface IRegistroRecorrenciaServico : IServicoBase<RegistroRecorrenciaModel>
    {
        ConsultaModel<RegistroRecorrenciaModel> Consultar(int idAtividade, IEnumerable<int> idList, DateTime? dataPrevista, DateTime? dataRealizacao,
            string ordenacao, bool crescente, int pagina, int quantidade);
        void FinalizarRegistroRecorrencia(int idAtividade, DateTime dataRealizacaoPrevista, List<ArquivoModel> evidencias);
    }
}
