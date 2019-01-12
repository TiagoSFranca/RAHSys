using System;
using System.Collections.Generic;
using RAHSys.Dominio.Servicos.Interfaces.Repositorios;
using RAHSys.Dominio.Servicos.Interfaces.Servicos;
using RAHSys.Entidades;
using RAHSys.Entidades.Entidades;
using System.Linq;
using RAHSys.Infra.CrossCutting.Exceptions;
using System.Globalization;

namespace RAHSys.Dominio.Servicos.Servicos
{
    public class PagamentoServico : ServicoBase<PagamentoModel>, IPagamentoServico
    {
        private readonly IContratoRepositorio _contratoRepositorio;
        private readonly IPagamentoRepositorio _pagamentoRepositorio;

        public PagamentoServico(IPagamentoRepositorio pagamentoRepositorio, IContratoRepositorio contratoRepositorio) : base(pagamentoRepositorio)
        {
            _pagamentoRepositorio = pagamentoRepositorio;
            _contratoRepositorio = contratoRepositorio;
        }

        public ConsultaModel<PagamentoModel> Consultar(int idContrato, IEnumerable<int> idList, string dataPagamento,
            string ordenacao, bool crescente, int pagina, int quantidade)
        {
            var consultaModel = new ConsultaModel<PagamentoModel>(pagina, quantidade);

            var query = _pagamentoRepositorio.Consultar().Where(e => e.IdContrato == idContrato);

            if (idList?.Count() > 0)
                query = query.Where(c => idList.Contains(c.IdPagamento));

            if (!string.IsNullOrEmpty(dataPagamento))
            {
                int mes = 0;
                int ano = 0;

                ValidarDataPagamento(dataPagamento, ref mes, ref ano);

                query = query.Where(e => e.DataPagamento.Month == mes && e.DataPagamento.Year == ano);
            }

            switch ((ordenacao ?? string.Empty).ToLower())
            {
                case "data":
                    query = crescente ? query.OrderBy(c => c.DataPagamento) : query.OrderByDescending(c => c.DataPagamento);
                    break;
                default:
                    query = crescente ? query.OrderBy(c => c.IdPagamento) : query.OrderByDescending(c => c.IdPagamento);
                    break;

            }
            var resultado = query.Skip((pagina == 1 ? 0 : pagina - 1) * quantidade).Take(quantidade).ToList();
            consultaModel.TotalItens = query.Count();
            consultaModel.Resultado = resultado;

            return consultaModel;
        }

        public void Adicionar(PagamentoModel obj)
        {
            ValidarContrato(obj.IdContrato);

            var menorData = DateTime.Parse("01/01/1901", new CultureInfo("pt-BR"), DateTimeStyles.None);
            obj.DataCriacao = DateTime.Now;
            if (VerificarExistenciaPagamento(obj))
                throw new CustomBaseException(new Exception(), string.Format("Pagamento já realizado para o mês [{0}/{1}]", obj.DataPagamento.Month, obj.DataPagamento.Year));
            if (obj.DataPagamento < menorData)
                throw new CustomBaseException(new Exception(), string.Format("Data [{0}/{1}] inválida", obj.DataPagamento.Month, obj.DataPagamento.Year));

            _pagamentoRepositorio.Adicionar(obj);
        }

        private void ValidarDataPagamento(string dataPagamento, ref int mes, ref int ano)
        {
            string erroDataInvalida = string.Format("Data [{0}] inválida", dataPagamento);
            var datas = dataPagamento.Split('/');

            if (datas.Count() != 2)
                throw new CustomBaseException(new Exception(), erroDataInvalida);

            if (!Int32.TryParse(datas[0], out mes))
                throw new CustomBaseException(new Exception(), erroDataInvalida);

            if (!Int32.TryParse(datas[1], out ano))
                throw new CustomBaseException(new Exception(), erroDataInvalida);

        }

        private bool VerificarExistenciaPagamento(PagamentoModel obj)
        {
            var query = _pagamentoRepositorio.Consultar()
                .Where(e => e.DataPagamento.Month == obj.DataPagamento.Month
                && e.DataPagamento.Year == obj.DataPagamento.Year
                && e.IdPagamento != obj.IdPagamento
                && e.IdContrato == obj.IdContrato);

            return query.Count() > 0;
        }

        private void ValidarContrato(int idContrato)
        {
            var contrato = _contratoRepositorio.ObterPorId(idContrato);
            if (contrato == null)
                throw new CustomBaseException(new Exception(), string.Format("Contrato de código [{0}] inválido", idContrato));
            if(contrato.AnaliseInvestimento?.Cliente == null)
                throw new CustomBaseException(new Exception(), string.Format("Contrato de código [{0}] ainda não foi assinado", idContrato));
        }
    }
}
