using RAHSys.Entidades;
using RAHSys.Entidades.Entidades;
using System.Collections.Generic;

namespace RAHSys.Dominio.Servicos.Interfaces.Servicos
{
    public interface IEvidenciaServico : IServicoBase<EvidenciaModel>
    {
        void AdicionarEvidencias(int idAtividade, int idRegistroRecorrencia, List<ArquivoModel> evidencias);
    }
}
