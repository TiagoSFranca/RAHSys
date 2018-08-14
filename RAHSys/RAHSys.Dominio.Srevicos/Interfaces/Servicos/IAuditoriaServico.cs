using RAHSys.Entidades;
using RAHSys.Entidades.Entidades;
using System;
using System.Collections.Generic;


namespace RAHSys.Dominio.Servicos.Interfaces.Servicos
{
    public interface IAuditoriaServico : IServicoAuditoriaBase<AuditoriaModel>
    {
        ConsultaModel<AuditoriaModel> Consultar(IEnumerable<int> idList, string usuario, string funcao, string acao, string enderecoIp, DateTime data,  string ordenacao, bool crescente, int pagina, int quantidade);
    }
}
