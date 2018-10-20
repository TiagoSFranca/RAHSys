using System.Collections.Generic;
using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Entidades.Entidades;
using System.Linq;
using RAHSys.Entidades;
using System;
using System.Globalization;

namespace RAHSys.Dominio.Servicos.Servicos
{
    public class AtividadeServico : ServicoBase<AtividadeModel>, IAtividadeServico
    {
        private readonly IAtividadeRepositorio _atividadeRepositorio;

        public AtividadeServico(IAtividadeRepositorio atividadeRepositorio) : base(atividadeRepositorio)
        {
            _atividadeRepositorio = atividadeRepositorio;
        }

        public ConsultaModel<AtividadeModel> Consultar(IEnumerable<int> idList, IEnumerable<int> idTipoAtividadeList, IEnumerable<int> idEquipeList,
            IEnumerable<int> idContratoList, IEnumerable<string> idUsuarioList, bool? realizada, string dataRealizacaoInicio, string dataRealizacaoFim,
            string dataPrevistaInicio, string dataPrevistaFim,
            string ordenacao, bool crescente, int pagina, int quantidade)
        {
            var consultaModel = new ConsultaModel<AtividadeModel>(pagina, quantidade);

            DateTime? dtRealizacaoInicio = ConvertStringToDate(dataRealizacaoInicio);
            DateTime? dtRealizacaoFim = ConvertStringToDate(dataRealizacaoFim);
            DateTime? dtPrevistaInicio = ConvertStringToDate(dataPrevistaInicio);
            DateTime? dtPrevistaFim = ConvertStringToDate(dataPrevistaFim);

            var query = _atividadeRepositorio.Consultar().Where(c => !c.Contrato.Excluido);
            if (idList?.Count() > 0)
                query = query.Where(c => idList.Contains(c.IdAtividade));

            if (idTipoAtividadeList?.Count() > 0)
                query = query.Where(c => idTipoAtividadeList.Contains(c.IdTipoAtividade));

            if (idEquipeList?.Count() > 0)
                query = query.Where(c => idEquipeList.Contains(c.IdEquipe));

            if (idContratoList?.Count() > 0)
                query = query.Where(c => idContratoList.Contains(c.IdContrato));

            if (idUsuarioList?.Count() > 0)
                query = query.Where(c => idUsuarioList.Contains(c.IdUsuario));

            if (realizada != null)
                query = query.Where(c => c.Realizada == realizada);

            if (dtRealizacaoFim != null)
                query = query.Where(c => c.DataRealizacao <= dtRealizacaoFim);

            if (dtRealizacaoInicio != null)
                query = query.Where(c => c.DataRealizacao >= dtRealizacaoInicio);

            if (dtPrevistaFim != null)
                query = query.Where(c => c.DataPrevista <= dtPrevistaFim);

            if (dtPrevistaInicio != null)
                query = query.Where(c => c.DataPrevista >= dtPrevistaInicio);

            switch ((ordenacao ?? string.Empty).ToLower())
            {
                case "tipoatividade":
                    query = crescente ? query.OrderBy(c => c.TipoAtividade.Descricao) : query.OrderByDescending(c => c.TipoAtividade.Descricao);
                    break;
                case "atribuidopara":
                    query = crescente ? query.OrderBy(c => c.Usuario.UserName) : query.OrderByDescending(c => c.Usuario.UserName);
                    break;
                case "datarealizacaoprevista":
                    query = crescente ? query.OrderBy(c => c.DataPrevista) : query.OrderByDescending(c => c.DataPrevista);
                    break;
                case "DataRealizacao":
                    query = crescente ? query.OrderBy(c => c.DataRealizacao) : query.OrderByDescending(c => c.DataRealizacao);
                    break;
                case "Realizada":
                    query = crescente ? query.OrderBy(c => c.Realizada) : query.OrderByDescending(c => c.Realizada);
                    break;
                default:
                    query = crescente ? query.OrderBy(c => c.IdAtividade) : query.OrderByDescending(c => c.IdAtividade);
                    break;
            }

            var resultado = query.Skip((pagina == 1 ? 0 : pagina - 1) * quantidade).Take(quantidade).ToList();
            consultaModel.TotalItens = query.Count();
            consultaModel.Resultado = resultado;

            return consultaModel;
        }

        private DateTime? ConvertStringToDate(string data)
        {
            if (DateTime.TryParseExact(data, "d/M/yyyy", new CultureInfo("pt-BR"), DateTimeStyles.None, out DateTime dataConvertida))
            {
                return dataConvertida;
            }
            return null;
        }
    }
}
