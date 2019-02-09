using RAHSys.Aplicacao.AppModels;
using System;
using System.Collections.Generic;

namespace RAHSys.Aplicacao.Interfaces
{
    public interface IRegistroRecorrenciaAppServico : IAppServicoBase<RegistroRecorrenciaAppModel>
    {
        ConsultaAppModel<RegistroRecorrenciaAppModel> Consultar(int idAtividade, IEnumerable<int> idList, DateTime? dataPrevista, DateTime? dataRealizacao,
            string ordenacao, bool crescente, int pagina, int quantidade);
        void FinalizarRegistroRecorrencia(int idAtividade, DateTime dataRealizacaoPrevista, List<ArquivoAppModel> evidencias);
    }
}
