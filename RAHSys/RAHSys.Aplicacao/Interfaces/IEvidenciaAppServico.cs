using RAHSys.Aplicacao.AppModels;
using System.Collections.Generic;

namespace RAHSys.Aplicacao.Interfaces
{
    public interface IEvidenciaAppServico : IAppServicoBase<EvidenciaAppModel>
    {
        void AdicionarEvidencias(int idAtividade, int idRegistroRecorrencia, List<ArquivoAppModel> evidencias);
    }
}
