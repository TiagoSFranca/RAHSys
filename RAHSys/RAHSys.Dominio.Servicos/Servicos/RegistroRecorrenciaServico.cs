using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Entidades;
using RAHSys.Entidades.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RAHSys.Dominio.Servicos.Servicos
{
    public class RegistroRecorrenciaServico : ServicoBase<RegistroRecorrenciaModel>, IRegistroRecorrenciaServico
    {
        private readonly IRegistroRecorrenciaRepositorio _registroRecorrenciaRepositorio;

        public RegistroRecorrenciaServico(IRegistroRecorrenciaRepositorio registroRecorrenciaRepositorio)
            : base(registroRecorrenciaRepositorio)
        {
            _registroRecorrenciaRepositorio = registroRecorrenciaRepositorio;
        }


        public ConsultaModel<RegistroRecorrenciaModel> Consultar(int idAtividade, IEnumerable<int> idList, DateTime? dataPrevista, DateTime? dataRealizacao, string ordenacao, bool crescente, int pagina, int quantidade)
        {
            var consultaModel = new ConsultaModel<RegistroRecorrenciaModel>(pagina, quantidade);

            var query = _registroRecorrenciaRepositorio.Consultar().Where(e => e.IdAtividade == idAtividade);

            if (idList?.Count() > 0)
                query.Where(e => idList.Contains(e.IdRegistroRecorrencia));

            if (dataPrevista.HasValue)
                query = query.Where(e => DbFunctions.TruncateTime(e.DataPrevista) == DbFunctions.TruncateTime(dataPrevista.Value));

            if (dataRealizacao.HasValue)
                query = query.Where(e => DbFunctions.TruncateTime(e.DataRealizacao) == DbFunctions.TruncateTime(dataRealizacao.Value));

            switch ((ordenacao ?? string.Empty).ToLower())
            {
                case "data":
                    query = crescente ? query.OrderBy(c => c.DataPrevista) : query.OrderByDescending(c => c.DataPrevista);
                    break;
                default:
                    query = crescente ? query.OrderBy(c => c.IdRegistroRecorrencia) : query.OrderByDescending(c => c.IdRegistroRecorrencia);
                    break;

            }

            var resultado = query.Skip((pagina == 1 ? 0 : pagina - 1) * quantidade).Take(quantidade).ToList();
            consultaModel.TotalItens = query.Count();
            consultaModel.Resultado = resultado;

            return consultaModel;
        }
    }
}
